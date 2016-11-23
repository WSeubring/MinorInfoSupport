using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lapiwe.AutoBeheerService.Facade
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Started: Lapiwe.AutoBeheerService");
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
