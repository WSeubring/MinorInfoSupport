using Lapiwe.GMS.FrontEnd.Controllers;
using Lapiwe.GMS.FrontEnd.DAL;
using Lapiwe.GMS.FrontEnd.Agents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lapiwe.GMS.FrontEnd.Agents.RDWAgent;
using Lapiwe.GMS.FrontEnd.Agents.RDWAgent.Models;

namespace Lapiwe.GMS.FrontEnd.Test.Controllers
{
    [TestClass]
    public class KeuringsVerzoekControllerTest
    {
        [TestMethod]
        public void KeuringsVerzoekControllerCanBeInstantiated()
        {
            // Arrange
            var agent = new Mock<IRDWAgent>(MockBehavior.Strict);
            var repository = new Mock<ISimpleRepository>(MockBehavior.Strict);
            KeuringsVerzoekController controller = new KeuringsVerzoekController(agent.Object, repository.Object);

            // Assert
            Assert.IsInstanceOfType(controller, typeof(Controller));
        }

        [TestMethod]
        public void InvullenReturnsAViewResult()
        {
            // Arrange
            var agent = new Mock<IRDWAgent>(MockBehavior.Strict);
            var repository = new Mock<ISimpleRepository>(MockBehavior.Strict);
            KeuringsVerzoekController controller = new KeuringsVerzoekController(agent.Object, repository.Object);

            // Act
            IActionResult result = controller.Invullen();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ToevoegenReturnsARedirectToAction()
        {
            // Arrange
            var agent = new Mock<IRDWAgent>(MockBehavior.Loose);
            var repository = new Mock<ISimpleRepository>(MockBehavior.Loose);
            KeuringsVerzoekController controller = new KeuringsVerzoekController(agent.Object, repository.Object);

            // Act
            IActionResult result = controller.Toevoegen("", "", "", "", 0);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void OverzichtReturnsAViewResult()
        {
            // Arrange
            var agent = new Mock<IRDWAgent>(MockBehavior.Loose);
            var repository = new Mock<ISimpleRepository>(MockBehavior.Loose);
            KeuringsVerzoekController controller = new KeuringsVerzoekController(agent.Object, repository.Object);

            // Act
            IActionResult result = controller.Overzicht();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
