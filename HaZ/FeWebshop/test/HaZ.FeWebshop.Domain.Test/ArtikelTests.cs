using HAZ.FeWebshop.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HaZ.FeWebshop.Domain.Test
{
    [TestClass]
    public class ArtikelTests
    {
        [TestMethod]
        public void ArtikelCalculateNewBtwWithNormalPrijs()
        {
            // Arrange
            var artikel = new Artikel();

            // Act
            artikel.Prijs = 100;

            // Assert
            Assert.AreEqual(121, artikel.PrijsInclBtw);
        }

        [TestMethod]
        public void ArtikelCalculateNewBtwWithCommaPrijs()
        {
            // Arrange
            var artikel = new Artikel();

            // Act
            artikel.Prijs = 100.10m;

            // Assert
            Assert.AreEqual(121.12m, artikel.PrijsInclBtw);
        }

        [TestMethod]
        public void ArtikelCalculateNewBtwWithPrijs0()
        {
            // Arrange
            var artikel = new Artikel();

            // Act
            artikel.Prijs = 0;

            // Assert
            Assert.AreEqual(0, artikel.PrijsInclBtw);
        }
    }
}
