using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Dag9.Events.Test
{
    [TestClass]
    public class WerknemerTest
    {
        [TestMethod]
        public void NieuweWerknemer()
        {
            //Act 
            var target = new Werknemer("Test", 10, 1000M);

            //Arrange
            Assert.AreEqual(1000M, target.Salaris);
        }

        [TestMethod]
        public void SalarisGaatOmhoogMetEenProcentAlsDeWerknemerEenJaarOuderWord()
        {
            //Act 
            var target = new Werknemer("Test", 10, 1000M);

            //Arrange
            Assert.AreEqual(1010M, target.Salaris);
        }

    }
}
