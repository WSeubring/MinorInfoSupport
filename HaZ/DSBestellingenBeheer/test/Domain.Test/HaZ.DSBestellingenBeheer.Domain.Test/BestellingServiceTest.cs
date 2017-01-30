using HaZ.DSBestellingenBeheer.Entities;
using HaZ.DSBestellingenBeheer.Incoming.Commands;
using HaZ.DSBestellingenBeheer.Outgoing.Events;
using HaZ.DSBestellingenBeheer.Services;
using HaZ.DSBestellingenBeheer.Services.Interfaces;
using InfoSupport.WSA.Common;
using InfoSupport.WSA.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaZ.DSBestellingenBeheer.Domain.Test
{
    [TestClass]
    public class BestellingServiceTest
    {
        [TestMethod]
        public void ServiceCallsRepoWithCorrectMappedBestelling()
        {
            // Arrange
            Bestelling result = null;
            var repoMock = new Mock<IRepository<Bestelling, int>>(MockBehavior.Strict);
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            
            repoMock.Setup(repo => repo.Insert(It.IsAny<Bestelling>())).Returns(1).Callback<Bestelling>(parameter => result = parameter);
            publisherMock.Setup(publisher => publisher.Publish(It.IsAny<BestellingToegevoegdEvent>()));
            var target = new BestellingService(repoMock.Object, publisherMock.Object);

            DateTime now = DateTime.UtcNow;
            BestellingToevoegenCommand correctCommand = new BestellingToevoegenCommand() {
                DatumBestelling = now,
                TotaalBedragInc = 10.0m,
                BestelStatus = "In behandeling",
                Klantgegevens = new Incoming.Commands.Klantgegevens() {
                    Naam = "Testnaam",
                    Huisnummer = "12a",
                    KlantId = 1,
                    Land = "Nederland",
                    Postcode = "1234AB",
                    Straatnaam = "Kalverstraat",
                    Woonplaats = "Amsterdam"
                },
                Bestelregels = new List<Incoming.Commands.Bestelregel>(){
                    new Incoming.Commands.Bestelregel(){
                        ArtikelId = 1,
                        AantalArtikelen = 5,
                        ArtikelBeschrijving = "Artikel beschrijving" ,
                        ArtikelNaam = "Test Artikel",
                        PrijsPerArtikelInc = 2.0m
                    }
                }
            };
        
            // Act
            target.BestellingToevoegen(correctCommand);

            // Assert
            repoMock.Verify(repo => repo.Insert(It.IsAny<Bestelling>()), Times.Once());
            Assert.AreEqual(now, result.DatumBestelling);
            Assert.AreEqual(correctCommand.TotaalBedragInc, result.TotaalBedragInc);
            Assert.AreEqual(correctCommand.BestelStatus, result.BestelStatus);
            Assert.AreEqual(correctCommand.Klantgegevens.Naam, result.Klantgegevens.Naam);
            Assert.AreEqual(correctCommand.Klantgegevens.Huisnummer, result.Klantgegevens.Huisnummer);
            Assert.AreEqual(correctCommand.Klantgegevens.KlantId, result.Klantgegevens.KlantId);
            Assert.AreEqual(correctCommand.Klantgegevens.Land, result.Klantgegevens.Land);
            Assert.AreEqual(correctCommand.Klantgegevens.Postcode, result.Klantgegevens.Postcode);
            Assert.AreEqual(correctCommand.Klantgegevens.Straatnaam, result.Klantgegevens.Straatnaam);
            Assert.AreEqual(correctCommand.Klantgegevens.Woonplaats, result.Klantgegevens.Woonplaats);
            Assert.AreEqual(correctCommand.Bestelregels.First().ArtikelId, result.Bestelregels.First().ArtikelId);
            Assert.AreEqual(correctCommand.Bestelregels.First().AantalArtikelen, result.Bestelregels.First().AantalArtikelen);
            Assert.AreEqual(correctCommand.Bestelregels.First().ArtikelBeschrijving, result.Bestelregels.First().ArtikelBeschrijving);
            Assert.AreEqual(correctCommand.Bestelregels.First().ArtikelNaam, result.Bestelregels.First().ArtikelNaam);
            Assert.AreEqual(correctCommand.Bestelregels.First().PrijsPerArtikelInc, result.Bestelregels.First().PrijsPerArtikelInc);
        }

        [TestMethod]
        public void BestellingToegevoegdEventPublished()
        {
            // Arrange
            Bestelling result = null;
            BestellingToegevoegdEvent thrownEvent = null;
            var repoMock = new Mock<IRepository<Bestelling, int>>(MockBehavior.Strict);
            var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);
            
            repoMock.Setup(repo => repo.Insert(It.IsAny<Bestelling>())).Returns(1).Callback<Bestelling>(parameter => result = parameter);
            publisherMock.Setup(publisher => publisher.Publish(It.IsAny<BestellingToegevoegdEvent>())).Callback<DomainEvent>(parameter => thrownEvent = (parameter as BestellingToegevoegdEvent));
            var target = new BestellingService(repoMock.Object, publisherMock.Object);

            DateTime now = DateTime.UtcNow;
            BestellingToevoegenCommand correctCommand = new BestellingToevoegenCommand()
            {
                DatumBestelling = now,
                TotaalBedragInc = 10.0m,
                BestelStatus = "In behandeling",
                Klantgegevens = new Incoming.Commands.Klantgegevens()
                {
                    Naam = "Testnaam",
                    Huisnummer = "12a",
                    KlantId = 1,
                    Land = "Nederland",
                    Postcode = "1234AB",
                    Straatnaam = "Kalverstraat",
                    Woonplaats = "Amsterdam"
                },
                Bestelregels = new List<Incoming.Commands.Bestelregel>(){
                    new Incoming.Commands.Bestelregel(){
                        ArtikelId = 1,
                        AantalArtikelen = 5,
                        ArtikelBeschrijving = "Artikel beschrijving" ,
                        ArtikelNaam = "Test Artikel",
                        PrijsPerArtikelInc = 2.0m
                    }
                }
            };

            // Act
            target.BestellingToevoegen(correctCommand);

            // Assert
            repoMock.Verify(repo => repo.Insert(It.IsAny<Bestelling>()), Times.Once());
            publisherMock.Verify(publisher => publisher.Publish(It.IsAny<BestellingToegevoegdEvent>()), Times.Once());

            Assert.AreEqual(now, thrownEvent.DatumBestelling);
            Assert.AreEqual(result.TotaalBedragInc, thrownEvent.TotaalBedragInc);
            Assert.AreEqual(result.BestelStatus, thrownEvent.BestelStatus);
            Assert.AreEqual(result.Klantgegevens.Naam, thrownEvent.Klantgegevens.Naam);
            Assert.AreEqual(result.Klantgegevens.Huisnummer, thrownEvent.Klantgegevens.Huisnummer);
            Assert.AreEqual(result.Klantgegevens.KlantId, thrownEvent.Klantgegevens.KlantId);
            Assert.AreEqual(result.Klantgegevens.Land, thrownEvent.Klantgegevens.Land);
            Assert.AreEqual(result.Klantgegevens.Postcode, thrownEvent.Klantgegevens.Postcode);
            Assert.AreEqual(result.Klantgegevens.Straatnaam, thrownEvent.Klantgegevens.Straatnaam);
            Assert.AreEqual(result.Klantgegevens.Woonplaats, thrownEvent.Klantgegevens.Woonplaats);
            Assert.AreEqual(result.Bestelregels.First().ArtikelId, thrownEvent.Bestelregels.First().ArtikelId);
            Assert.AreEqual(result.Bestelregels.First().AantalArtikelen, thrownEvent.Bestelregels.First().AantalArtikelen);
            Assert.AreEqual(result.Bestelregels.First().ArtikelBeschrijving, thrownEvent.Bestelregels.First().ArtikelBeschrijving);
            Assert.AreEqual(result.Bestelregels.First().ArtikelNaam, thrownEvent.Bestelregels.First().ArtikelNaam);
            Assert.AreEqual(result.Bestelregels.First().PrijsPerArtikelInc, thrownEvent.Bestelregels.First().PrijsPerArtikelInc);
        }
    }
}
