using HAZ.FeWebshop.Api.Controllers;
using HAZ.FeWebshop.Domain.Entities;
using Microsoft.Extensions.Logging;
using HAZ.FeWebshop.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HAZ.FeWebshop.Api.UnitTests
{
    [TestClass]
    public class ArtikelenControllerTests
    {
        [TestMethod]
        public void GetArtikelenCheckCallsWithSuccesfullArtikelen()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ArtikelenController>>(MockBehavior.Strict);
            var artikelServiceMock = new Mock<IArtikelService>(MockBehavior.Strict);
            artikelServiceMock.Setup(service => service.GetAllArtikelen()).Returns(new List<Artikel> { new Artikel() });

            var target = new ArtikelenController(loggerMock.Object, artikelServiceMock.Object);

            // Act
            var result = target.GetAllArtikelen();

            // Assert
            artikelServiceMock.Verify(service => service.GetAllArtikelen(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void GetArtikelenUnexpectedExceptionOccured()
        {
            // Arrange
            var artikelServiceMock = new Mock<IArtikelService>(MockBehavior.Strict);
            artikelServiceMock.Setup(service => service.GetAllArtikelen()).Throws<Exception>();
            var loggerMock = new Mock<ILogger<ArtikelenController>>();

            var target = new ArtikelenController(loggerMock.Object, artikelServiceMock.Object);

            // Act
            var result = target.GetAllArtikelen();
            
            // Assert
            artikelServiceMock.Verify(service => service.GetAllArtikelen(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(500, (result as StatusCodeResult).StatusCode);
        }
    }
}
