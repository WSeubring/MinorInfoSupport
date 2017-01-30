using HAZ.PsWinkelen.Api.Test.DefaultData;
using HAZ.PsWinkelen.Exporting.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Api.Test.Domain
{
    [TestClass]
    public class ArtikelTest
    {
        [TestMethod]
        public void TestArtikelReplayEventWithoutBTW()
        {
            // Arrange
            var artikelAanCatalogusToegevoegd = DefaultData.DefaultData.ArtikelAanCatalogusToegevoegdEvent();
            var artikel = new Artikel();

            // Act
            artikel.ReplayCatalogusToegevoegdEvent(artikelAanCatalogusToegevoegd);

            // Assert
            Assert.AreEqual(artikelAanCatalogusToegevoegd.AfbeeldingUrl, artikel.AfbeeldingUrl);
            Assert.AreEqual(artikelAanCatalogusToegevoegd.Artikelnummer, artikel.Artikelnummer);
            Assert.AreEqual(artikelAanCatalogusToegevoegd.Beschrijving, artikel.Beschrijving);
            Assert.AreEqual(artikelAanCatalogusToegevoegd.Leverancier, artikel.Leverancier);
            Assert.AreEqual(artikelAanCatalogusToegevoegd.LeverancierCode, artikel.LeverancierCode);
            Assert.AreEqual(artikelAanCatalogusToegevoegd.LeverbaarTot, artikel.LeverbaarTot);
            Assert.AreEqual(artikelAanCatalogusToegevoegd.LeverbaarVanaf, artikel.LeverbaarVanaf);
            Assert.AreEqual(artikelAanCatalogusToegevoegd.Naam, artikel.Naam);
            Assert.AreEqual(artikelAanCatalogusToegevoegd.Prijs, artikel.Prijs);
            Assert.IsTrue(artikel.InCatalog);
        }

        [TestMethod]
        public void TestArtikelReplayEventBTWPart()
        {
            // Arrange
            var artikelAanCatalogusToegevoegd = DefaultData.DefaultData.ArtikelAanCatalogusToegevoegdEvent();
            var artikel = new Artikel();
            var btwPrijs = artikelAanCatalogusToegevoegd.Prijs * (1 + ((decimal)21 / 100)); // 21% btw calculation

            // Act
            artikel.ReplayCatalogusToegevoegdEvent(artikelAanCatalogusToegevoegd);

            // Assert
            Assert.AreEqual(artikelAanCatalogusToegevoegd.Prijs, artikel.Prijs);
            Assert.AreNotEqual(artikel.PrijsInclBtw, artikel.Prijs);
            Assert.AreEqual(btwPrijs, artikel.PrijsInclBtw);
        }

        [TestMethod]
        public void TestArtikelReplayEventBTWPartWithCommaPrice()
        {
            // Arrange
            var artikelAanCatalogusToegevoegd = DefaultData.DefaultData.ArtikelAanCatalogusToegevoegdEvent();
            var artikel = new Artikel();
            artikelAanCatalogusToegevoegd.Prijs = 10.45m;
            var btwPrijs = artikelAanCatalogusToegevoegd.Prijs * (1 + ((decimal)21 / 100)); // 21% btw calculation

            // Act
            artikel.ReplayCatalogusToegevoegdEvent(artikelAanCatalogusToegevoegd);

            // Assert
            Assert.AreEqual(artikelAanCatalogusToegevoegd.Prijs, artikel.Prijs);
            Assert.AreNotEqual(artikel.PrijsInclBtw, artikel.Prijs);
            Assert.AreEqual(btwPrijs, artikel.PrijsInclBtw);
        }

        [TestMethod]
        public void TestArtikelReplayEventBTWPartWith0Price()
        {
            // Arrange
            var artikelAanCatalogusToegevoegd = DefaultData.DefaultData.ArtikelAanCatalogusToegevoegdEvent();
            var artikel = new Artikel();
            artikelAanCatalogusToegevoegd.Prijs = 0m;
            
            // Act
            artikel.ReplayCatalogusToegevoegdEvent(artikelAanCatalogusToegevoegd);

            // Assert
            Assert.AreEqual(artikelAanCatalogusToegevoegd.Prijs, artikel.Prijs);
            Assert.AreEqual(0m, artikel.PrijsInclBtw);
        }
    }
}
