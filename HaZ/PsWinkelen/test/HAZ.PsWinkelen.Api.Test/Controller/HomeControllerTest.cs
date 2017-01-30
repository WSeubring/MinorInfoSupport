using HAZ.PsWinkelen.API.Controllers;
using HAZ.PsWinkelen.Domain.Infrastructure.Agents;
using HAZ.PsWinkelen.Domain.Models;
using HAZ.PsWinkelen.Domain.Services;
using HAZ.PsWinkelen.Exporting.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace HAZ.PsWinkelen.Api.Test
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void HomeControllerReturnsCorrectKlantInformation()
        {
            //Arrange
            var bestellingenBeheerServiceAgentMock = new Mock<IBestellingenBeheerServiceAgent>(MockBehavior.Strict);
            var loggerMock = new Mock<ILogger<HAZ.PsWinkelen.API.Controllers.HomeController>>(MockBehavior.Strict);
            var artikelServiceMock = new Mock<IArtikelService>(MockBehavior.Strict);

            List<Artikel> testArtikelen = new List<Artikel>();
            var artikelAanCatalogusToegevoegd = DefaultData.DefaultData.ArtikelAanCatalogusToegevoegdEvent();
            var artikel = new Artikel();
            artikel.ReplayCatalogusToegevoegdEvent(artikelAanCatalogusToegevoegd);
            testArtikelen.Add(artikel);
            
            artikelServiceMock.Setup(asm => asm.GetArtikel(324)).Returns(artikel);
            bestellingenBeheerServiceAgentMock.Setup(bbsa => bbsa.PostBestellingToevoegen(It.IsAny<BestellingToevoegenCommand>())).Returns(new object());
            
            FullBestelling commandStartBestellingCommand = new FullBestelling
            {
                Artikelen = testArtikelen,
                Klant = new Klant
                {
                    Naam = "Piet Hendriks",
                    KlantId = 0,
                    Straatnaam = "Saturnus",
                    Huisnummer = "365",
                    Land = "Nederland",
                    Plaats = "Duiven",
                    Postcode = "6912 AD"
                }
            };
            HomeController controller = new HomeController(bestellingenBeheerServiceAgentMock.Object, artikelServiceMock.Object, loggerMock.Object);

            //Act
            ObjectResult response = controller.StartBestelling(commandStartBestellingCommand);
            BestellingResult bestellingResult = (BestellingResult) response.Value;

            //Assert
            Assert.AreEqual(true, bestellingResult.IsValid);
            //klant
            Assert.AreEqual(commandStartBestellingCommand.Klant.Naam, bestellingResult.Bestelling.Klant.Naam);
            Assert.AreEqual(commandStartBestellingCommand.Klant.Plaats, bestellingResult.Bestelling.Klant.Plaats);
            Assert.AreEqual(commandStartBestellingCommand.Klant.Postcode, bestellingResult.Bestelling.Klant.Postcode);
            Assert.AreEqual(commandStartBestellingCommand.Klant.Straatnaam, bestellingResult.Bestelling.Klant.Straatnaam);
            Assert.AreEqual(commandStartBestellingCommand.Klant.Land, bestellingResult.Bestelling.Klant.Land);
            Assert.AreEqual(commandStartBestellingCommand.Klant.Huisnummer, bestellingResult.Bestelling.Klant.Huisnummer);
            Assert.AreEqual(commandStartBestellingCommand.Klant.KlantId, bestellingResult.Bestelling.Klant.KlantId);
        }

        [TestMethod]
        public void HomeControllerReturnsCorrectArticleInformation()
        {
            //Arrange
            var bestellingenBeheerServiceAgentMock = new Mock<IBestellingenBeheerServiceAgent>(MockBehavior.Strict);
            var loggerMock = new Mock<ILogger<HAZ.PsWinkelen.API.Controllers.HomeController>>(MockBehavior.Strict);
            var artikelServiceMock = new Mock<IArtikelService>(MockBehavior.Strict);

            List<Artikel> testArtikelen = new List<Artikel>();
            var artikelAanCatalogusToegevoegd = DefaultData.DefaultData.ArtikelAanCatalogusToegevoegdEvent();
            var artikel = new Artikel();
            artikel.ReplayCatalogusToegevoegdEvent(artikelAanCatalogusToegevoegd);
            testArtikelen.Add(artikel);

            artikelServiceMock.Setup(asm => asm.GetArtikel(324)).Returns(artikel);
            bestellingenBeheerServiceAgentMock.Setup(bbsa => bbsa.PostBestellingToevoegen(It.IsAny<BestellingToevoegenCommand>())).Returns(new object());

            FullBestelling commandStartBestellingCommand = new FullBestelling
            {
                Artikelen = testArtikelen,
                Klant = new Klant
                {
                    Naam = "Piet Hendriks",
                    KlantId = 0,
                    Straatnaam = "Saturnus",
                    Huisnummer = "365",
                    Land = "Nederland",
                    Plaats = "Duiven",
                    Postcode = "6912 AD"
                }
            };
            HomeController controller = new HomeController(bestellingenBeheerServiceAgentMock.Object, artikelServiceMock.Object, loggerMock.Object);

            //Act
            ObjectResult response = controller.StartBestelling(commandStartBestellingCommand);
            BestellingResult bestellingResult = (BestellingResult)response.Value;

            //Assert
            Assert.AreEqual(true, bestellingResult.IsValid);
            //correct
            List<Artikel> artikels = (List<Artikel>) commandStartBestellingCommand.Artikelen;
            Artikel ar = artikels.Find(a => a.Artikelnummer == artikel.Artikelnummer);
            //returned artikel
            List<Artikel> returnArtikels = (List<Artikel>)bestellingResult.Bestelling.Artikelen;
            Artikel returnedAr = returnArtikels.Find(a => a.Artikelnummer == artikel.Artikelnummer);

            Assert.AreEqual(ar.Artikelnummer, returnedAr.Artikelnummer);
            Assert.AreEqual(ar.AfbeeldingUrl, returnedAr.AfbeeldingUrl);
            Assert.AreEqual(ar.Beschrijving, returnedAr.Beschrijving);
            Assert.AreEqual(ar.InCatalog, returnedAr.InCatalog);
            Assert.AreEqual(ar.LeverbaarVanaf, returnedAr.LeverbaarVanaf);
            Assert.AreEqual(ar.LeverbaarTot, returnedAr.LeverbaarTot);
            Assert.AreEqual(ar.Naam, returnedAr.Naam);
            Assert.AreEqual(ar.Leverancier, returnedAr.Leverancier);
            Assert.AreEqual(ar.Prijs, returnedAr.Prijs);
            Assert.AreEqual(ar.PrijsInclBtw, returnedAr.PrijsInclBtw);
            Assert.AreEqual(ar.LeverancierCode, returnedAr.LeverancierCode);
        }

        [TestMethod]
        public void HomeControllerReturnsCorrectNumberOfProducts()
        {
            //Arrange
            var bestellingenBeheerServiceAgentMock = new Mock<IBestellingenBeheerServiceAgent>(MockBehavior.Strict);
            var loggerMock = new Mock<ILogger<HomeController>>(MockBehavior.Strict);
            var artikelServiceMock = new Mock<IArtikelService>(MockBehavior.Strict);

            List<Artikel> testArtikelen = new List<Artikel>();
            var artikelAanCatalogusToegevoegd = DefaultData.DefaultData.ArtikelAanCatalogusToegevoegdEvent();
            var artikel = new Artikel();
            artikel.ReplayCatalogusToegevoegdEvent(artikelAanCatalogusToegevoegd);
            testArtikelen.Add(artikel);
            testArtikelen.Add(artikel);

            artikelServiceMock.Setup(asm => asm.GetArtikel(324)).Returns(artikel);

            BestellingToevoegenCommand res = null;
            bestellingenBeheerServiceAgentMock
                .Setup(bbsa => bbsa.PostBestellingToevoegen(It.IsAny<BestellingToevoegenCommand>()))
                .Returns(new object())
                .Callback<BestellingToevoegenCommand>(c => res = c);

            FullBestelling commandStartBestellingCommand = new FullBestelling
            {
                Artikelen = testArtikelen,
                Klant = new Klant
                {
                    Naam = "Piet Hendriks",
                    KlantId = 0,
                    Straatnaam = "Saturnus",
                    Huisnummer = "365",
                    Land = "Nederland",
                    Plaats = "Duiven",
                    Postcode = "6912 AD"
                }
            };
            HomeController controller = new HomeController(bestellingenBeheerServiceAgentMock.Object, artikelServiceMock.Object, loggerMock.Object);

            //Act
            ObjectResult response = controller.StartBestelling(commandStartBestellingCommand);

            //Assert
            foreach (Bestelregel bestregel in res.Bestelregels)
            {
                Assert.AreEqual(2, bestregel.AantalArtikelen);
            }
        }

        [TestMethod]
        public void HomeControllerReturnsCorrectNumberOfProductsMore()
        {
            //Arrange
            var bestellingenBeheerServiceAgentMock = new Mock<IBestellingenBeheerServiceAgent>(MockBehavior.Strict);
            var loggerMock = new Mock<ILogger<HAZ.PsWinkelen.API.Controllers.HomeController>>(MockBehavior.Strict);
            var artikelServiceMock = new Mock<IArtikelService>(MockBehavior.Strict);

            List<Artikel> testArtikelen = new List<Artikel>();
            var artikelAanCatalogusToegevoegd = DefaultData.DefaultData.ArtikelAanCatalogusToegevoegdEvent();
            var artikel = new Artikel();
            artikel.ReplayCatalogusToegevoegdEvent(artikelAanCatalogusToegevoegd);
            testArtikelen.Add(artikel);
            testArtikelen.Add(artikel);
            testArtikelen.Add(artikel);
            testArtikelen.Add(artikel);
            testArtikelen.Add(artikel);

            artikelServiceMock.Setup(asm => asm.GetArtikel(324)).Returns(artikel);

            BestellingToevoegenCommand res = null;
            bestellingenBeheerServiceAgentMock
                .Setup(bbsa => bbsa.PostBestellingToevoegen(It.IsAny<BestellingToevoegenCommand>()))
                .Returns(new object())
                .Callback<BestellingToevoegenCommand>(c => res = c);

            FullBestelling commandStartBestellingCommand = new FullBestelling
            {
                Artikelen = testArtikelen,
                Klant = new Klant
                {
                    Naam = "Piet Hendriks",
                    KlantId = 0,
                    Straatnaam = "Saturnus",
                    Huisnummer = "365",
                    Land = "Nederland",
                    Plaats = "Duiven",
                    Postcode = "6912 AD"
                }
            };
            HomeController controller = new HomeController(bestellingenBeheerServiceAgentMock.Object, artikelServiceMock.Object, loggerMock.Object);

            //Act
            ObjectResult response = controller.StartBestelling(commandStartBestellingCommand);

            //Assert
            foreach (Bestelregel bestregel in res.Bestelregels)
            {
                Assert.AreEqual(5, bestregel.AantalArtikelen);
            }
        }

        [TestMethod]
        public void StartBestellingUnexpectedErrorOccured()
        {
            // Arrange
            var logger = new Mock<ILogger<HomeController>>();
            var artikelService = new Mock<IArtikelService>(MockBehavior.Strict);
            artikelService.Setup(service => service.GetArtikel(It.IsAny<int>())).Throws<Exception>();
            var bestellingAgent = new Mock<IBestellingenBeheerServiceAgent>(MockBehavior.Strict);
            var controller = new HomeController(bestellingAgent.Object, artikelService.Object, logger.Object);

            // Act
            var result = controller.StartBestelling(DefaultData.DefaultData.StartBestellingCommand());

            // Assert
            var objectResult = (result as ObjectResult);
            var statusCodeResult = (objectResult.Value as StatusCodeResult);
            Assert.AreEqual(500 ,statusCodeResult.StatusCode);
        }
    }
}
