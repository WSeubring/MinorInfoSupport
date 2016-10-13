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
        public void CurrentWeekRedirectGeeftRedirectToAction()
        {
            //Arrange
            var mockAgent = new MockCursusInstantieAgent();
            var target = new CursusController(mockAgent);

            //Act
            var result = target.CurrentWeekRedirect();

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void IndexGeeftActionResultMetViewModel()
        {
            //Arrange
            var mockAgent = new MockCursusInstantieAgent();
            var target = new CursusController(mockAgent);

            //Act
            var result = target.Index(2015, 1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(CursusOverzichtViewModel));
            Assert.AreEqual(1, mockAgent.AantalCallsOpGetMetJaarEnWeek);
            Assert.AreEqual(2015, mockAgent.LaatstMeegegeveJaarInGet);
            Assert.AreEqual(1, mockAgent.LaatstMeegegeveWeekInGet);

        }

        [TestMethod]
        public void ImportenCallsAddFromFileWithEmptyFile()
        {
            //Arrange
            var mockAgent = new MockCursusInstantieAgent();
            var target = new CursusController(mockAgent);

            //Act
            var result = target.Importeren(new ImporterenViewModel() { File = new FormFile(new MemoryStream(), 0, 0, "TestFile", "TestName") });

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(1, mockAgent.AantalCallsOpAddFromFile);
        }

        [TestMethod]
        public void ImporterenGeeftEenLeegModelInViewResult()
        {
            //Arrange
            var mockAgent = new MockCursusInstantieAgent();
            var target = new CursusController(mockAgent);

            //Act
            var result = target.Importeren();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(ImporterenViewModel));

        }

        [TestMethod]
        public void IndexGeeftCursusOverzichtModel()
        {
            //Arrange
            var mockAgent = new MockCursusInstantieAgent();
            var target = new CursusController(mockAgent);

            //Act
            var result = target.Importeren();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(ImporterenViewModel));
        }

        [TestMethod]
        public void IndexFilterdOpWeek()
        {
            //Arrange
            var mockAgent = new MockCursusInstantieAgent();
            var target = new CursusController(mockAgent);

            //Act
            var result = target.Index(2016, 10);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(CursusOverzichtViewModel));
        }

        [TestMethod]
        public void IndexWeek53GeeftRedirect()
        {
            //Arrange
            var mockAgent = new MockCursusInstantieAgent();
            var target = new CursusController(mockAgent);

            //Act
            var result = target.Index(2016, 53);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(2017, (result as RedirectToActionResult).RouteValues["jaar"]);
            Assert.AreEqual(1, (result as RedirectToActionResult).RouteValues["week"]);
        }

        [TestMethod]
        public void IndexWeek0GeeftRedirect()
        {
            //Arrange
            var mockAgent = new MockCursusInstantieAgent();
            var target = new CursusController(mockAgent);

            //Act
            var result = target.Index(2016, 0);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(2015, (result as RedirectToActionResult).RouteValues["jaar"]);
            Assert.AreEqual(52, (result as RedirectToActionResult).RouteValues["week"]);
        }
    }
}
