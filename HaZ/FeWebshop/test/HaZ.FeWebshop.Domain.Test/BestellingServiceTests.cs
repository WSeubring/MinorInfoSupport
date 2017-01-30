using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Domain.Infrastructure.Agents;
using HAZ.FeWebshop.Domain.Infrastructure.Repositories;
using HAZ.FeWebshop.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace HaZ.FeWebshop.Domain.Test
{
    [TestClass]
    public class BestellingServiceTests
    {
        private Bestelling _correctBestelling;
        private int _dummyArtikelId;

        [TestInitialize]
        public void initialize()
        {
            _dummyArtikelId = 5;
            _correctBestelling = new Bestelling
            {
                Artikelen = new List<int> { _dummyArtikelId },
                Klant = new Klant
                {
                    Naam = "Hans Worst",
                    Land = "Nederland",
                    Postcode = "4385ID",
                    Huisnummer = "56a",
                    Straatnaam = "FrederickStraat",
                    Plaats = "Breda"
                }
            };
        }


        [TestMethod]
        public void PlaatsBestellingCallsWinkelAgent()
        {
            // Arrange
            var artikelRepositoryMock = new Mock<IArtikelRepository>(MockBehavior.Strict);
            artikelRepositoryMock.Setup(m => m.Find(_dummyArtikelId)).Returns(new Artikel());
            var artikelService = new ArtikelService(artikelRepositoryMock.Object);

            int dummyKlantId = 3;

            var winkelAgentMock = new Mock<IWinkelenAgent>(MockBehavior.Strict);
            winkelAgentMock.Setup(
                m => m.PlaatsBestelling(
                    It.IsAny<FullBestelling>()))
                    .Returns(
                        new BestellingResult
                        {
                            Bestelling = new FullBestelling
                            {
                                Artikelen = new List<Artikel>(),
                                Klant = new Klant { KlantId = dummyKlantId }
                            }
                        }
                    );

            var bestellingService = new BestellingService(winkelAgentMock.Object, artikelService);

            // Act
            var bestellingResult = bestellingService.PlaatsBestelling(_correctBestelling);

            // Assert
            Assert.AreEqual(dummyKlantId, bestellingResult.Bestelling.Klant.KlantId);
        }

        [TestMethod]
        public void PlaatsBestellingInvalidArtikelReturnsErrorMessageAndDoesNotCallWinkelAgent()
        {
            // Arrange
            var artikelRepositoryMock = new Mock<IArtikelRepository>(MockBehavior.Strict);
            artikelRepositoryMock.Setup(m => m.Find(_dummyArtikelId)).Returns((Artikel)null);
            var artikelService = new ArtikelService(artikelRepositoryMock.Object);

            var winkelAgentMock = new Mock<IWinkelenAgent>(MockBehavior.Strict);
            winkelAgentMock.Setup(
                m => m.PlaatsBestelling(
                    It.IsAny<FullBestelling>())
            );

            var bestellingService = new BestellingService(winkelAgentMock.Object, artikelService);

            // Act
            var bestellingResult = bestellingService.PlaatsBestelling(_correctBestelling);

            // Assert
            Assert.AreEqual(1, bestellingResult.Errors.Count);
            Assert.AreEqual("Artikel " + _dummyArtikelId + " is niet meer beschikbaar", bestellingResult.Errors[0]);
        }

        [TestMethod]
        public void PlaatsBestellingContainsInvalidArtikelReturnsErrorMessageAndDoesNotCallWinkelAgent()
        {
            // Arrange
            int invalidArtikelNummer = _dummyArtikelId + 1;

            var artikelRepositoryMock = new Mock<IArtikelRepository>(MockBehavior.Strict);
            artikelRepositoryMock.Setup(m => m.Find(_dummyArtikelId)).Returns(new Artikel());
            artikelRepositoryMock.Setup(m => m.Find(invalidArtikelNummer)).Returns((Artikel)null);
            var artikelService = new ArtikelService(artikelRepositoryMock.Object);

            var winkelAgentMock = new Mock<IWinkelenAgent>(MockBehavior.Strict);
            winkelAgentMock.Setup(
                m => m.PlaatsBestelling(
                    It.IsAny<FullBestelling>())
            );

            var bestellingService = new BestellingService(winkelAgentMock.Object, artikelService);

            _correctBestelling.Artikelen = new List<int> { _dummyArtikelId, invalidArtikelNummer };

            // Act
            var bestellingResult = bestellingService.PlaatsBestelling(_correctBestelling);

            // Assert
            Assert.AreEqual(1, bestellingResult.Errors.Count);
            Assert.AreEqual("Artikel " + invalidArtikelNummer + " is niet meer beschikbaar", bestellingResult.Errors[0]);
        }

        [TestMethod]
        public void PlaatsBestellingReturnsCorrectErrorMessages()
        {
            // Arrange
            int invalidArtikelNummer = _dummyArtikelId + 1;
            int invalidArtikelNummer2 = invalidArtikelNummer + 1;

            var artikelRepositoryMock = new Mock<IArtikelRepository>(MockBehavior.Strict);
            artikelRepositoryMock.Setup(m => m.Find(_dummyArtikelId)).Returns(new Artikel());
            artikelRepositoryMock.Setup(m => m.Find(invalidArtikelNummer)).Returns((Artikel)null);
            artikelRepositoryMock.Setup(m => m.Find(invalidArtikelNummer2)).Returns((Artikel)null);
            var artikelService = new ArtikelService(artikelRepositoryMock.Object);

            var winkelAgentMock = new Mock<IWinkelenAgent>(MockBehavior.Strict);
            winkelAgentMock.Setup(
                m => m.PlaatsBestelling(
                    It.IsAny<FullBestelling>())
            );

            var bestellingService = new BestellingService(winkelAgentMock.Object, artikelService);

            _correctBestelling.Artikelen = new List<int> { _dummyArtikelId, invalidArtikelNummer, invalidArtikelNummer2 };

            // Act
            var bestellingResult = bestellingService.PlaatsBestelling(_correctBestelling);

            // Assert
            Assert.AreEqual(2, bestellingResult.Errors.Count);
            Assert.AreEqual("Artikel " + invalidArtikelNummer + " is niet meer beschikbaar", bestellingResult.Errors[0]);
            Assert.AreEqual("Artikel " + invalidArtikelNummer2 + " is niet meer beschikbaar", bestellingResult.Errors[1]);
        }

        [TestMethod]
        public void BestellingResultIsInvalidIfBestellingIsNull()
        {
            // Arrange
            var bestellingResult = new BestellingResult { Bestelling = null };

            // Act 
            var result = bestellingResult.IsValid;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BestellingResultIsValidIfBestellingIsNotNull()
        {
            // Arrange
            var bestellingResult = new BestellingResult { Bestelling = new FullBestelling() };

            // Act 
            var result = bestellingResult.IsValid;

            // Assert
            Assert.IsTrue(result);
        }
    }
}
