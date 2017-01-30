using HaZ.DSBestellingenBeheer.Outgoing.Events;
using HAZ.FeBestellingen.Domain.Entities;
using HAZ.FeBestellingen.Domain.Test.DefautData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace HAZ.FeBestellingen.Domain.Test
{
    [TestClass]
    public class BestellingTests
    {
        [TestMethod]
        public void TestBestelingReplayEvent()
        {
            // Arrange
            var bestellingToegevoegdEvent = Defaultdata.BestellingToegevoegdEvent();
            var bestelling = new Bestelling();

            // Act
            bestelling.ReplayBestellingToegevoegdEvent(bestellingToegevoegdEvent);
            
            // Assert Bestelling
            Assert.AreEqual(bestellingToegevoegdEvent.Bestelnummer , bestelling.Bestelnummer);
            Assert.AreEqual(bestellingToegevoegdEvent.BestelStatus , bestelling.BestelStatus);
            Assert.AreEqual(bestellingToegevoegdEvent.DatumBestelling , bestelling.DatumBestelling);
            Assert.AreEqual(bestellingToegevoegdEvent.TotaalBedragExc, bestelling.TotaalBedragExc);
            Assert.AreEqual(bestellingToegevoegdEvent.TotaalBedragInc, bestelling.TotaalBedragInc);

            //Assert Bestelling.Bestelregels
            var bestelregelExpected = bestellingToegevoegdEvent.Bestelregels.First();
            var bestelregelResult = bestelling.Bestelregels.First();
            Assert.AreEqual(bestelregelExpected.AantalArtikelen, bestelregelResult.AantalArtikelen);
            Assert.AreEqual(bestelregelExpected.AfbeeldingUrl, bestelregelResult.AfbeeldingUrl);
            Assert.AreEqual(bestelregelExpected.ArtikelBeschrijving, bestelregelResult.ArtikelBeschrijving);
            Assert.AreEqual(bestelregelExpected.ArtikelId, bestelregelResult.ArtikelId);
            Assert.AreEqual(bestelregelExpected.ArtikelNaam, bestelregelResult.ArtikelNaam);
            Assert.AreEqual(bestelregelExpected.BestelregelId, bestelregelResult.BestelregelId);

            //Assert Bestelling.Klantgegevns
            var klantgegevensExpected = bestellingToegevoegdEvent.Klantgegevens;
            var klantgegevensResult = bestelling.Klantgegevens;
            Assert.AreEqual(klantgegevensExpected.Huisnummer, klantgegevensResult.Huisnummer);
            Assert.AreEqual(klantgegevensExpected.KlantgegevensId, klantgegevensResult.KlantgegevensId);
            Assert.AreEqual(klantgegevensExpected.KlantId, klantgegevensResult.KlantId);
            Assert.AreEqual(klantgegevensExpected.Land, klantgegevensResult.Land);
            Assert.AreEqual(klantgegevensExpected.Naam, klantgegevensResult.Naam);
            Assert.AreEqual(klantgegevensExpected.Postcode, klantgegevensResult.Postcode);
            Assert.AreEqual(klantgegevensExpected.Straatnaam, klantgegevensResult.Straatnaam);
            Assert.AreEqual(klantgegevensExpected.Woonplaats, klantgegevensResult.Woonplaats);
        }
    }
}
