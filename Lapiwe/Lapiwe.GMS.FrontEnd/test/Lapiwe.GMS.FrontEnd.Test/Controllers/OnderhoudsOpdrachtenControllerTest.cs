using Lapiwe.GMS.FrontEnd.Agents;
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

namespace Lapiwe.GMS.FrontEnd.Test.Controllers
{
    [TestClass]
    public class OnderhoudsOpdrachtenControllerTest
    {
        [TestMethod]
        public void OnderhoudsControllerCanBeInstantiated()
        {
            // Arrange
            var agent = new Mock<IOnderhoudsServiceAgent>(MockBehavior.Strict);
            var repository = new Mock<ISimpleRepository>(MockBehavior.Strict);
            OnderhoudsOpdrachtenController controller = new OnderhoudsOpdrachtenController(agent.Object, repository.Object);

            // Assert
            Assert.IsInstanceOfType(controller, typeof(Controller));
        }

        [TestMethod]
        public void InvullenReturnsAViewResult()
        {
            // Arrange
            var agent = new Mock<IOnderhoudsServiceAgent>(MockBehavior.Strict);
            var repository = new Mock<ISimpleRepository>(MockBehavior.Strict);
            OnderhoudsOpdrachtenController controller = new OnderhoudsOpdrachtenController(agent.Object, repository.Object);

            // Act
            IActionResult result = controller.Invullen();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ToevoegenReturnsARedirectToAction()
        {
            // Arrange
            var agent = new Mock<IOnderhoudsServiceAgent>(MockBehavior.Loose);
            var repository = new Mock<ISimpleRepository>(MockBehavior.Loose);
            OnderhoudsOpdrachtenController controller = new OnderhoudsOpdrachtenController(agent.Object, repository.Object);

            // Act
            IActionResult result = controller.Toevoegen("piet", "123", "abc-123", 0, "olie checken", false);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void OverzichtReturnsAViewResult()
        {
            // Arrange
            var agent = new Mock<IOnderhoudsServiceAgent>(MockBehavior.Loose);
            var repository = new Mock<ISimpleRepository>(MockBehavior.Loose);
            OnderhoudsOpdrachtenController controller = new OnderhoudsOpdrachtenController(agent.Object, repository.Object);

            // Act
            IActionResult result = controller.Overzicht();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ZoekReturnsView()
        {
            // Arrange
            var agent = new Mock<IOnderhoudsServiceAgent>(MockBehavior.Loose);
            var repository = new Mock<ISimpleRepository>(MockBehavior.Loose);
            OnderhoudsOpdrachtenController controller = new OnderhoudsOpdrachtenController(agent.Object, repository.Object);

            // Act
            IActionResult result = controller.Zoek();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
