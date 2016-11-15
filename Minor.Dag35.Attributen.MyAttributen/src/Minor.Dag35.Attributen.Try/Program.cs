using Minor.Dag35.Attributen.MyAttributen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Dag35.Attributen.Try
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }

        [Test(1, Output = 1)]
        [Test(5, Output = 5)]
        [Test(1, Output = 2)]
        [Test(5, Output = 9)]
        [Test("WOWOWW", Output = 10)]
        [Test("", -110, "s", ExpectedException = typeof(ArgumentOutOfRangeException))]
        [Test(-1, -110, ExpectedException = typeof(ArgumentOutOfRangeException))]
        public int HelloWorld(int i)
        {
            return i;
        }
    }
}