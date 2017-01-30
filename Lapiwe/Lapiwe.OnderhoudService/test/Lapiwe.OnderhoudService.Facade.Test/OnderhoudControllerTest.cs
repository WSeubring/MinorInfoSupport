using Lapiwe.Common.Infastructure;
using Lapiwe.OnderhoudService.Domain;
using Lapiwe.OnderhoudService.Export;
using Lapiwe.OnderhoudService.Facade.Controllers;
using Lapiwe.OnderhoudService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.OnderhoudService.Facade.Test
{
    [TestClass]
    public class OnderhoudControllerTest
    {

        [TestMethod]
        public void OnderhoudController_MaakNieuwOnderhoudsOpdracht_Fails()
        {
            // Arrange
            var repoMock = new Mock<IRepository>(MockBehavior.Strict);
            var pubMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            var target = new OnderhoudController(repoMock.Object, pubMock.Object);

            // Act
            IActionResult result = target.MaakNieuwOnderhoudsOpdracht(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            repoMock.Verify(r => r.Insert(It.IsAny<OnderhoudsOpdracht>()), Times.Never());
            pubMock.Verify(p => p.Publish(It.IsAny<OnderhoudsOpdrachtAangemeldEvent>()), Times.Never());
        }

        [TestMethod]
        public void OnderhoudController_MaakNieuwOnderhoudsOpdracht_Success()
        {
            // Arrange
            var repoMock = new Mock<IRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Insert(It.IsAny<OnderhoudsOpdracht>()));

            var pubMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            pubMock.Setup(p => p.Publish(It.IsAny<OnderhoudsOpdrachtAangemeldEvent>()));

            var target = new OnderhoudController(repoMock.Object, pubMock.Object);

            var command = new MeldOnderhoudsOpdrachtAanCommand();

            // Act
            IActionResult result = target.MaakNieuwOnderhoudsOpdracht(command);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            repoMock.Verify(r => r.Insert(It.IsAny<OnderhoudsOpdracht>()), Times.Once());
            pubMock.Verify(p => p.Publish(It.IsAny<OnderhoudsOpdrachtAangemeldEvent>()), Times.Once());
        }

        [TestMethod]
        public void OnderhoudController_MaakNieuwOnderhoudsOpdracht_SuccessWithData()
        {
            // Arrange
            var repoMock = new Mock<IRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Insert(It.IsAny<OnderhoudsOpdracht>()));

            var pubMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            pubMock.Setup(p => p.Publish(It.IsAny<OnderhoudsOpdrachtAangemeldEvent>()));

            var target = new OnderhoudController(repoMock.Object, pubMock.Object);

            var command = new MeldOnderhoudsOpdrachtAanCommand()
            {
                KlantGuid = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                AutoGuid = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                AanmeldDatum = DateTime.ParseExact("2016-11-30 10:51", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Kilometerstand = 101,
                OpdrachtOmschrijving = "test omschrijving",
                Apk = true
            };

            // Act
            IActionResult result = target.MaakNieuwOnderhoudsOpdracht(command);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            repoMock.Verify(r => r.Insert(It.IsAny<OnderhoudsOpdracht>()), Times.Once());
            pubMock.Verify(p => p.Publish(It.IsAny<OnderhoudsOpdrachtAangemeldEvent>()), Times.Once());
        }

        [TestMethod]
        public void OnderhoudController_StartNieuwOnderhoudsOpdracht_Fail()
        {
            // Arrange
            var repoMock = new Mock<IRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Find(It.IsAny<Guid>()));
            repoMock.Setup(r => r.Update(It.IsAny<OnderhoudsOpdracht>()));

            var pubMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            pubMock.Setup(p => p.Publish(It.IsAny<OnderhoudsOpdrachtGestartEvent>()));

            var target = new OnderhoudController(repoMock.Object, pubMock.Object);

            // Act
            IActionResult result = target.StartNieuwOnderhoudsOpdracht(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            repoMock.Verify(r => r.Find(It.IsAny<Guid>()), Times.Never());
            repoMock.Verify(r => r.Update(It.IsAny<OnderhoudsOpdracht>()), Times.Never());
            pubMock.Verify(p => p.Publish(It.IsAny<OnderhoudsOpdrachtGestartEvent>()), Times.Never());
        }

        [TestMethod]
        public void OnderhoudController_StartNieuwOnderhoudsOpdracht_Success()
        {
            // Arrange
            var repoMock = new Mock<IRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Find(It.IsAny<Guid>())).Returns(new OnderhoudsOpdracht() { Guid = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), OpdrachtStatus = Status.Onderhoud });
            repoMock.Setup(r => r.Update(It.IsAny<OnderhoudsOpdracht>()));

            var pubMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            pubMock.Setup(p => p.Publish(It.IsAny<OnderhoudsOpdrachtGestartEvent>()));

            var target = new OnderhoudController(repoMock.Object, pubMock.Object);

            var command = new StartOnderhoudOpdrachtCommand(new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"));

            // Act
            IActionResult result = target.StartNieuwOnderhoudsOpdracht(command);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            repoMock.Verify(r => r.Find(It.IsAny<Guid>()), Times.Once());
            repoMock.Verify(r => r.Update(It.IsAny<OnderhoudsOpdracht>()), Times.Once());
            pubMock.Verify(p => p.Publish(It.IsAny<OnderhoudsOpdrachtGestartEvent>()), Times.Once());
        }


    }
}
