using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case1.AdministratieCursusenCursistenApi.Exceptions;
using Minor.Case1.AdministratieCursusenCursistenApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursistenApiTest.Services
{
    [TestClass]
    public class CursusTextParserTest
    {
        private readonly string _folderPath = @"Services/TestTextFiles";

        [TestMethod]
        public void Parser1CursusInstantie()
        {
            //Arrange
            var target = new CursusTextParser();
            var text = string.Join(Environment.NewLine,
                "Titel: C# Programmeren",
                "Cursuscode: CNETIN",
                "Duur: 5 dagen",
                "Startdatum: 13/10/2014");

            //Act
            var result = target.Parse(text).Single();

            //Assert
            Assert.AreEqual("C# Programmeren", result.Cursus.Titel);
           // Assert.AreEqual("CNETIN", result.CursusCode);
            Assert.AreEqual(5, result.Cursus.Duur);
            Assert.AreEqual(new DateTime(2014, 10, 13), result.StartDatum);
        }


        [TestMethod]
        public void ParseMet2ItemsInTextFile()
        {
            //Arrange
            var target = new CursusTextParser();
            var text = string.Join(Environment.NewLine,
                "Titel: TestTitel",
                "Cursuscode: TESTCODE",
                "Duur: 5 dagen",
                "Startdatum: 11/10/2016",
                "",
                "Titel: TestTitel2",
                "Cursuscode: TESTCODE2",
                "Duur: 3 dagen",
                "Startdatum: 01/11/2016"
                );

            //Act
            var result = target.Parse(text);

            //Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void InvalidTitelThrowsSyntaxException()
        {
            //Arrange
            var target = new CursusTextParser();
            var text = string.Join(Environment.NewLine,
                "titeel: TestTitel",
                "Cursuscode: TESTCODE",
                "Duur: 5 dagen",
                "Startdatum: 11/10/2016",
                "",
                "Titel: TestTitel2",
                "Cursuscode: TESTCODE2",
                "Duur: 3 dagen",
                "Startdatum: 01/11/2016"
                );

            //Act
            Action action = () => target.Parse(text);

            //Assert
            Assert.ThrowsException<SyntaxException>(action);
        }

        [TestMethod]
        public void InvalidTitelTeLang()
        {
            //Arrange
            var target = new CursusTextParser();

            string path = Path.Combine(_folderPath, @"InvalidTitelToLong.txt");
            var text = File.ReadAllText(path);
               
            //Act
            Action action = () => target.Parse(text);

            //Assert
            Assert.ThrowsException<SyntaxException>(action);
        }

        [TestMethod]
        public void InvalidCursusCodeSyntaxIncorrect()
        {
            //Arrange
            var target = new CursusTextParser();

            string path = Path.Combine(_folderPath, @"InvalidSyntaxCursusCode.txt");
            var text = File.ReadAllText(path);

            //Act
            Action action = () => target.Parse(text);

            //Assert
            Assert.ThrowsException<SyntaxException>(action);
        }


        [TestMethod]
        public void InvalidCursusCodeTeLang()
        {
            //Arrange
            var target = new CursusTextParser();

            string path = Path.Combine(_folderPath, @"InvalidCursusCodeToLong.txt");
            var text = File.ReadAllText(path);

            //Act
            Action action = () => target.Parse(text);

            //Assert
            Assert.ThrowsException<SyntaxException>(action);
        }

        [TestMethod]
        public void InvalidDuurVan6Dagen()
        {
            //Arrange
            var target = new CursusTextParser();

            string path = Path.Combine(_folderPath, @"InvalidDuurVan6Dagen.txt");
            var text = File.ReadAllText(path);

            //Act
            Action action = () => target.Parse(text);

            //Assert
            Assert.ThrowsException<SyntaxException>(action);
        }

        [TestMethod]
        public void InvalidDatum()
        {
            //Arrange
            var target = new CursusTextParser();

            string path = Path.Combine(_folderPath, @"InvalidDatum.txt");
            var text = File.ReadAllText(path);

            //Act
            Action action = () => target.Parse(text);

            //Assert
            Assert.ThrowsException<SyntaxException>(action);
        }

        [TestMethod]
        public void InvalidSyntaxThrowExceptionMetLijnNummer()
        {
            //Arrange
            var target = new CursusTextParser();

            string path = Path.Combine(_folderPath, @"InvalidSyntaxLijn8.txt");
            var text = File.ReadAllText(path);

            //Act
            try
            {
                target.Parse(text);
                
            }
            catch (SyntaxException ex)
            {
                //Assert
                Assert.AreEqual("Regel: 6 voldoet niet aan de syntax.", ex.Message);
            }
        }

        [TestMethod]
        public void ArgumentNullExceptionBijLegeFile()
        {
            //Arrange
            var target = new CursusTextParser();

            var text = "";

            //Act
            Action action = () => target.Parse(text);
      
            //Assert
            Assert.ThrowsException<ArgumentNullException>(action);
            
        }

        [TestMethod]
        public void Invalid2LegeRegelsNaCursusInstantie()
        {
            string path = Path.Combine(_folderPath, @"Invalid2LegeRegelsNaCursusInstantie.txt");
            var text = File.ReadAllText(path);

            var target = new CursusTextParser();

            //Act
            Action action = () => target.Parse(text);

            //Assert
            Assert.ThrowsException<SyntaxException>(action);

        }
    }
}
