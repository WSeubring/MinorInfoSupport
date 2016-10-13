using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursistenApiTest.Entiteiten
{
    [TestClass]
    public class CursusInstantieTest
    {
        [TestMethod]
        public void TestEqualsOnKeyEqual()
        {
            //Arrange
            var cursusInstantie1 = new CursusInstantie()
            {
                StartDatum = new DateTime(2016, 1, 1),
                Cursus = new Cursus()
                {
                    Code = "CODE",
                    Duur = 1,
                    Titel= "Titel"
                }
            };

            var cursusInstantie2 = new CursusInstantie()
            {
                StartDatum = new DateTime(2016, 1, 1),
                Cursus = new Cursus()
                {
                    Code = "CODE",
                    Duur = 1,
                    Titel = "Titel"
                }
            };

            //Act
            var result = cursusInstantie1.Equals(cursusInstantie2);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEqualsOnKeyNotEqual()
        {
            //Arrange
            var cursusInstantie1 = new CursusInstantie()
            {
                StartDatum = new DateTime(2016, 1, 1),
                Cursus = new Cursus()
                {
                    Code = "CODE",
                    Duur = 1,
                    Titel = "Titel"
                }
            };

            var cursusInstantie2 = new CursusInstantie()
            {
                StartDatum = new DateTime(2016, 1, 1),
                Cursus = new Cursus()
                {
                    Code = "CODE2",
                    Duur = 1,
                    Titel = "Titel"
                }
            };

            //Act
            var result = cursusInstantie1.Equals(cursusInstantie2);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
