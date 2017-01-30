using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using HAZ.FeWebshop.Api.Controllers;
using HAZ.FeWebshop.Domain.Entities;
using System.Collections.Generic;
using HAZ.FeWebshop.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace HAZ.FeWebshop.Api.Test
{
    [TestClass]
    public class BestellingenControllerTests
    {
        [TestMethod]
        public void PlaatsValidBestelling()
        {
            // Arrange
            Bestelling bestelling = new Bestelling
            {
                Artikelen = new List<int>() { 3 },
                Klant = new Klant
                {
                    Huisnummer = "123ab",
                    KlantId = 2,
                    Land = "Nederland",
                    Naam = "Hans Worst",
                    Plaats = "Utrecht",
                    Postcode = "1234AB",
                    Straatnaam = "Hansworststraat"
                }
            };

            var loggerMock = new Mock<ILogger<BestellingenController>>();

            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            bestellingServiceMock.Setup(m => m.PlaatsBestelling(It.IsAny<Bestelling>()))
                .Returns(new BestellingResult
                {
                    Bestelling = new FullBestelling
                    {
                        Artikelen = new List<Artikel>()
                        {
                            new Artikel
                            {
                                Artikelnummer = 3
                            }
                        },
                        Klant = bestelling.Klant
                    }
                });
            var controller = new BestellingenController(
                loggerMock.Object,
                bestellingServiceMock.Object);

            // Act
            CreatedResult result = (CreatedResult)controller.PlaatsBestelling(bestelling);
            FullBestelling fullBestelling = (FullBestelling)result.Value;

            // Assert
            Assert.AreEqual(bestelling.Klant.KlantId, fullBestelling.Klant.KlantId);
        }

        [TestMethod]
        public void PlaatsInValidBestellingResult()
        {
            // Arrange
            Bestelling bestelling = new Bestelling
            {
                Artikelen = new List<int>() { 3 },
                Klant = new Klant
                {
                    Huisnummer = "123ab",
                    KlantId = 2,
                    Land = "Nederland",
                    Naam = "Hans Worst",
                    Plaats = "Utrecht",
                    Postcode = "1234AB",
                    Straatnaam = "Hansworststraat"
                }
            };

            var loggerMock = new Mock<ILogger<BestellingenController>>();

            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            bestellingServiceMock.Setup(m => m.PlaatsBestelling(It.IsAny<Bestelling>()))
                .Returns(new BestellingResult
                {
                    Bestelling = null
                });
            var controller = new BestellingenController(
                loggerMock.Object,
                bestellingServiceMock.Object);

            // Act
            IActionResult result = controller.PlaatsBestelling(bestelling);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void PlaatsInValidBestelling()
        {
            // Arrange
            Bestelling bestelling = new Bestelling();

            var loggerMock = new Mock<ILogger<BestellingenController>>();

            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);

            var controller = new BestellingenController(
                loggerMock.Object,
                bestellingServiceMock.Object);
            controller.ModelState.AddModelError("", "");

            // Act
            IActionResult result = controller.PlaatsBestelling(bestelling);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}