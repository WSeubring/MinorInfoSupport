using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case1.AdministratieCursusenCursistenApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursistenApiTest.Services
{
    [TestClass]
    public class CursusTextParserTest
    {
        [TestMethod]
        public void Parser1CursusInstantie()
        {
            //Arrange
            var target = new CursusTextParser();
            var text = string.Join(Environment.NewLine,
                "Titel: C# Programmeren",
                "Cursuscode: CNETIN ",
                "Duur: 5 dagen",
                "Startdatum: 13/10/2014");

            //Act
            var result = target.Parse(text);

            //Assert
            Assert.AreEqual(1, result.Count());
        }
    }
}
