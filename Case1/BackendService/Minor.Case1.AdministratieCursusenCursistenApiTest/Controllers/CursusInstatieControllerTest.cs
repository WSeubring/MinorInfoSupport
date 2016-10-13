using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case1.AdministratieCursusenCursistenApi.Controllers;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Minor.Case1.AdministratieCursusenCursistenApiTest
{
    [TestClass]
    public class CursusInstatieControllerTest
    {
        
        [TestMethod]
        public void Get()
        {
            //Arrange
            var mockRepository = new MockCursusInstantieRepository();
            var mockCursusTextFileParser = new CursusTextParserMock();
            var target = new CursusInstantieController(mockRepository, mockCursusTextFileParser);

            //Act
            target.Get();

            //Assert
            Assert.AreEqual(1, mockRepository.AantalCallsOpFindAll);

        }

        public void GetBy()
        {
            //Arrange
            var mockRepository = new MockCursusInstantieRepository();
            var mockCursusTextFileParser = new CursusTextParserMock();
            var target = new CursusInstantieController(mockRepository, mockCursusTextFileParser);

            //
            target.GetByYearAndWeek(2016, 2);

            //Assert
            Assert.AreEqual(1, mockRepository.AantalCallsFindBy);
        }

        [TestMethod]
        public void AddFromTextFileWithEmptyTextFile()
        {
            //Arrange
            var mockRepository = new MockCursusInstantieRepository();
            var mockCursusTextFileParser = new CursusTextParserMock();
            var target = new CursusInstantieController(mockRepository, mockCursusTextFileParser);

            //Act
            var result = target.AddFromTextFile("");

            //Assert
            Assert.AreEqual(0, mockRepository.AantalCallsOpFindAll);
            Assert.AreEqual(0, mockRepository.CallsOpAddRange.Count());
            Assert.AreEqual(1, mockCursusTextFileParser.CallsOpParse.Count());
        }

        [TestMethod]
        public void AddFromTextFileWithOneItem()
        {
            //Arrange
            var mockRepository = new MockCursusInstantieRepository();
            var mockCursusTextFileParser = new CursusTextParserMock();
            var target = new CursusInstantieController(mockRepository, mockCursusTextFileParser);
            
            //Act
            var result = target.AddFromTextFile("TestItem");

            //Assert
            Assert.AreEqual(1, mockRepository.AantalCallsOpFindAll);
            Assert.AreEqual(1, mockRepository.CallsOpAddRange.Count());
            Assert.IsInstanceOfType(result, typeof(AddFromFileResultReport));
            Assert.AreEqual(1, (result as AddFromFileResultReport).AantalAddedItems);
            Assert.AreEqual(0, (result as AddFromFileResultReport).AantalDuplicateItems);
        }
        [TestMethod]
        public void AddFromTextFileWithSyntaxError()
        {
            //Arrange
            var mockRepository = new MockCursusInstantieRepository();
            var mockCursusTextFileParser = new CursusTextParserMock();
            var target = new CursusInstantieController(mockRepository, mockCursusTextFileParser);

            //Act
            var result = target.AddFromTextFile("SyntaxError");

            //Assert
            Assert.AreEqual(0, mockRepository.AantalCallsOpFindAll);
            Assert.AreEqual(0, mockRepository.CallsOpAddRange.Count());
            Assert.IsInstanceOfType(result, typeof(AddFromFileResultReport));
            Assert.AreEqual(0, (result as AddFromFileResultReport).AantalAddedItems);
            Assert.AreEqual(0, (result as AddFromFileResultReport).AantalDuplicateItems);
            Assert.AreEqual("Fout op regel: 4", (result as AddFromFileResultReport).ErrorMessage);
        }


        [TestMethod]
        public void AmmountOfDuplicates()
        {
            //Arrange
            var mockRepository = new MockCursusInstantieRepository();
            var mockCursusTextFileParser = new CursusTextParserMock();
            var target = new CursusInstantieController(mockRepository, mockCursusTextFileParser);

            //Act
            var result = target.AddFromTextFile("2 Files uit mock waarbij 1 duplicate");

            //Assert
            Assert.AreEqual(1, result.AantalAddedItems);
            Assert.AreEqual(1, mockRepository.CallsOpAddRange.Single().Count());

            Assert.IsInstanceOfType(result, typeof(AddFromFileResultReport));
            Assert.AreEqual(1, (result as AddFromFileResultReport).AantalAddedItems);
            Assert.AreEqual(1, (result as AddFromFileResultReport).AantalDuplicateItems);
        }

        [TestMethod]
        public void GetByYearAndWeek()
        {
            //Arrange
            var mockRepository = new MockCursusInstantieRepository();
            var mockCursusTextFileParser = new CursusTextParserMock();
            var target = new CursusInstantieController(mockRepository, mockCursusTextFileParser);

            //Act
            var result = target.GetByYearAndWeek(2016, 1);

            //Assert
            Assert.AreEqual(1, mockRepository.AantalCallsFindBy);
        }
    }
}
