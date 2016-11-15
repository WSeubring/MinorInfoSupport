using Minor.Dag35.Attributen.MyAttributen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Minor.Dag35.Attributen.TestTool
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var assembly = Assembly.Load(new AssemblyName("Minor.Dag35.Attributen.Try"));
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                foreach (var method in methods)
                {
                    var testAttributes = method.GetCustomAttributes<TestAttribute>();

                    foreach (var test in testAttributes)
                    {
                        if (test.ExpectedException != null)
                        {
                            HandelExeptionTest(test, method, type);
                        }
                        else if (test.Argument != null)
                        {
                            HandelTest(test, method, type);
                        }
                    }
                }
            }
        }

        private static void HandelTest(TestAttribute test, MethodInfo method, Type type)
        {
            var instance = Activator.CreateInstance(type);
            object[] Arguments = { test.Argument };

            if (ValidateInputType(method, test.Argument.GetType()))
            {
                object result = method.Invoke(instance, Arguments);

                if (result.Equals(test.Output))
                {
                    PrintSuccesMessage(method.Name, test.Argument.ToString(), test.Output.ToString());
                }
                else
                {
                    PrintFailMessage(method.Name, test.Output.ToString(), result.ToString());
                }
            }
        }



        private static void HandelExeptionTest(TestAttribute test, MethodInfo method, Type type)
        {
            var instance = Activator.CreateInstance(type);
            object[] Arguments = test.Arguments;


            foreach (var argument in Arguments)
            {
                if (ValidateInputType(method, argument.GetType()))
                {
                    object[] paramerters = { argument };

                    try
                    {
                        method.Invoke(instance, paramerters);
                        PrintFailMessage(method.Name, test.ExpectedException.ToString(), "none exception thrown");
                    }

                    catch (Exception ex)
                    {
                        if (ex.GetType() == test.ExpectedException)
                        {
                            PrintSuccesMessage(method.Name, argument.ToString(), test.ExpectedException.ToString());
                        }
                        else
                        {
                            PrintFailMessage(method.Name, test.ExpectedException.ToString(), ex.GetType().ToString());
                        }
                    }
                }
            }
        }

        private static bool ValidateInputType(MethodInfo method, Type inputType)
        {
            var parameters = method.GetParameters();
            if (parameters[0].ParameterType != inputType)
            {
                PrintFailMessage(method.Name, $"{parameters[0].ParameterType} as input type", $"type is {inputType}");
                return false;
            }
            return true;
        }

        private static void PrintFailMessage(string name, string expected, string actual)
        {
            Console.WriteLine($"[FAILED] for {name} expected {expected} actual {actual}");
        }

        private static void PrintSuccesMessage(string name, string input, string result)
        {
            Console.WriteLine($"[Success] for {name} with input {input} and expected result {result} ");
        }

    }
}
