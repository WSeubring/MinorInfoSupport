using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minor.Case1.AdministratieCursusenCursisten.Controllers;
using Minor.Case1.AdministratieCursusenCursisten.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Case1.AdministratieCursusenCursistenTests
{
    [TestClass]
    public class CursusControllerTest
    {
        [TestMethod]
        public void IndexGeeftActionResultMetViewModel()
        {
            //Arrange
            var mockAgent = new MockCursusInstantieAgent();
            var target = new CursusController(mockAgent);

            //Act
            var result = target.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(List<CursusOverzicht>));
            Assert.AreEqual(1, mockAgent.AantalCallsOpGet);
        }

        [TestMethod]
        public void ImportenCallsAddFromFileWithEmptyFile()
        {
            //Arrange
            var mockAgent = new MockCursusInstantieAgent();
            var target = new CursusController(mockAgent);

            //Act
            var result = target.Importeren(new FormFile(new MemoryStream(), 0, 0, "TestFile", "TestName"));

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(1, mockAgent.AantalCallsOpAddFromFile);
        }

        [TestMethod]
        public void ImportenWithOutAFile()
        {
            //Arrange
            var mockAgent = new MockCursusInstantieAgent();
            var target = new CursusController(mockAgent);

            //Act
            var result = target.Importeren(null);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(1, mockAgent.AantalCallsOpAddFromFile);
        }
    }
}
