using Lapiwe.Common.Infastructure;
using Lapiwe.KlantBeheerService.Domain;
using Lapiwe.KlantBeheerService.Export;
using Lapiwe.KlantBeheerService.Facade.Controllers;
using Lapiwe.KlantBeheerService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.KlantBeheerService.Facade.Test
{
    [TestClass]
    public class KlantControllerTest
    {
        [TestMethod]
        public void KlantController_CanBeInstantiated()
        {
            var repo = new Mock<IRepository>(MockBehavior.Strict);
            var pubMock = new Mock<IEventPublisher>(MockBehavior.Strict);

            Controller target = new KlantController(repo.Object, pubMock.Object);

            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void KlantController_MaakNieuweKlant_Fails()
        {
            // Arrange
            var target = new KlantController(null, null);

            // Act
            IActionResult result = target.MaakNieuweKlant(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void KlantController_MaakNieuweKlant_Success()
        {
            // Arrange
            var repoMock = new Mock<IRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Insert(It.IsAny<Klant>()));

            var pubMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            pubMock.Setup(p => p.Publish(It.IsAny<KlantIsAangemaaktEvent>()));

            var target = new KlantController(repoMock.Object, pubMock.Object);

            var command = new MaakNieuweKlantCommand();

            // Act
            IActionResult result = target.MaakNieuweKlant(command);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            repoMock.Verify(r => r.Insert(It.IsAny<Klant>()), Times.Once());
            pubMock.Verify(p => p.Publish(It.IsAny<KlantIsAangemaaktEvent>()), Times.Once());
        }
    }
}
