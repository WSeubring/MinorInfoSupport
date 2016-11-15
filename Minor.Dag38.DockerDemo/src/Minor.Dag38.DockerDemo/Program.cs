using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Minor.Dag38.DockerDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            for(int i = 1; i <= 500; i++)
            {
                Console.WriteLine("HeelOOOO, Docker");
                Thread.Sleep(10);
            }
        }
    }
}
