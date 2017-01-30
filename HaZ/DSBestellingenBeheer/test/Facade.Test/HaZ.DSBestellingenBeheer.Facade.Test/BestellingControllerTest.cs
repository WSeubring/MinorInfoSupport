using HaZ.DSBestellingenBeheer.Entities;
using HaZ.DSBestellingenBeheer.Facade.Controllers;
using HaZ.DSBestellingenBeheer.Facade.Errors;
using HaZ.DSBestellingenBeheer.Incoming.Commands;
using HaZ.DSBestellingenBeheer.Services;
using HaZ.DSBestellingenBeheer.Services.Interfaces;
using InfoSupport.WSA.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaZ.DSBestellingenBeheer.Facade.Test
{
    [TestClass]
    public class BestellingControllerTest
    {
        [TestMethod]
        public void BestellingControllerReturnsAnOkResultWhenACorrectCommandEnters()
        {
            // Arrange
            var repoMock = new Mock<IRepository<Bestelling, int>>(MockBehavior.Strict);
            var loggerMock = new Mock<ILogger<BestellingenController>>();
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            var bestellingServiceMock = new Mock<BestellingService>(MockBehavior.Strict, repoMock.Object, publisherMock.Object);
            bestellingServiceMock.Setup(service => service.BestellingToevoegen(It.IsAny<BestellingToevoegenCommand>()));
            bestellingServiceMock.Setup(service => service.Dispose());
            var target = new BestellingenController(bestellingServiceMock.Object, loggerMock.Object);
            BestellingToevoegenCommand correctCommand = new BestellingToevoegenCommand() { };
           
            // Act
            var result = target.BestellingToevoegen(correctCommand);
            var resultObject = result as OkResult;

            // Assert
            Assert.IsTrue(result is OkResult);
            Assert.AreEqual(200, resultObject.StatusCode);
            bestellingServiceMock.Verify(bestellingService => bestellingService.BestellingToevoegen(It.IsAny<BestellingToevoegenCommand>()), Times.Once());
        }

        [TestMethod]
        public void BestellingControllerReturnsAnBadRequestWhenABadCommandEnters()
        {
            // Arrange
            var repoMock = new Mock<IRepository<Bestelling, int>>(MockBehavior.Strict);
            var loggerMock = new Mock<ILogger<BestellingenController>>();
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            var bestellingServiceMock = new Mock<BestellingService>(MockBehavior.Strict, repoMock.Object, publisherMock.Object);
            bestellingServiceMock.Setup(service => service.Dispose());
            bestellingServiceMock.Setup(service => service.BestellingToevoegen(It.IsAny<BestellingToevoegenCommand>())).Throws(new ArgumentException("A test Exception"));
            var target = new BestellingenController(bestellingServiceMock.Object, loggerMock.Object);
            BestellingToevoegenCommand wrongCommand = new BestellingToevoegenCommand() { };

            // Act
            var result = target.BestellingToevoegen(wrongCommand);

            // Assert'm
            var resultObject = result as BadRequestObjectResult;
            var resultValueObject = resultObject.Value as ErrorMessage;

            Assert.IsTrue(result is BadRequestObjectResult);
            Assert.AreEqual(400, resultObject.StatusCode);
            Assert.AreEqual( ErrorType.BadRequest , resultValueObject.FoutType);
            Assert.IsTrue(resultValueObject.FoutMelding.Contains("A error occured"));
            bestellingServiceMock.Verify(bestellingService => bestellingService.BestellingToevoegen(It.IsAny<BestellingToevoegenCommand>()), Times.Once());
        }
    }
}
