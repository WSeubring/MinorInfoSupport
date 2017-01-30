using HAZ.FeWebshop.Api.Controllers;
using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Api.UnitTests
{
    [TestClass]
    public class BestellingControllerTests
    {
        [TestMethod]
        public void PlaatsBestellingUnexpectedErrorOccured()
        {
            // Arrange
            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            bestellingServiceMock.Setup(service => service.PlaatsBestelling(It.IsAny<Bestelling>())).Throws<Exception>();
            var loggerMock = new Mock<ILogger<BestellingenController>>();

            var target = new BestellingenController(loggerMock.Object, bestellingServiceMock.Object);

            // Act
            var result =  target.PlaatsBestelling(new Bestelling());

            // Assert
            bestellingServiceMock.Verify(service => service.PlaatsBestelling(It.IsAny<Bestelling>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(500, (result as StatusCodeResult).StatusCode);
        }
    }
}
