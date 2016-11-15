using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Dag35.Attributen.MyAttributen
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TestAttribute : Attribute
    {
        public object Output { get; set; }
        public Type ExpectedException { get; set; }
        public object Argument { get;}
        public object[] Arguments { get; }

        public TestAttribute(object argument)
        {
            Argument = argument;
        }

        public TestAttribute(params object[] arguments)
        {
            Arguments = arguments;
        }
    }
}
