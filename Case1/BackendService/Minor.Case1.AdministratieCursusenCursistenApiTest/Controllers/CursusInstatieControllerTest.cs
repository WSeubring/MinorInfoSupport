using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case1.AdministratieCursusenCursistenApi.Controllers;
using System;
using System.Linq;

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
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
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
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void AmmountOfDuplicates()
        {
            //Arrange
            var mockRepository = new MockCursusInstantieRepository();
            var mockCursusTextFileParser = new CursusTextParserMock();
            var target = new CursusInstantieController(mockRepository, mockCursusTextFileParser);

            //Act
            var result = target.AddFromTextFile("2 Files uit mock");

            //Assert
            Assert.AreEqual((new OkObjectResult("{ nAddedItems:1, nDuplicateItems:1 }").Value), (result as OkObjectResult).Value);
            Assert.AreEqual(1, mockRepository.CallsOpAddRange.Single().Count());

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
