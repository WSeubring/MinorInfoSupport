using HaZ.FeWebshop.Domain.Test.Utils;
using HAZ.FeWebshop.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace HaZ.FeWebshop.Domain.Test
{
    [TestClass]
    public class BestellingValidationTests
    {
        private Bestelling _bestelling;

        [TestInitialize]
        public void InitializeCorrectBestelling()
        {
            // Arrange
            List<int> artikelNummers = new List<int> { 1, 4, 8, 40 };
            Guid guid = Guid.NewGuid();
            Klant klant = new Klant
            {
                Naam = "Hans Worst",
                Land = "Nederland",
                Postcode = "4385ID",
                Huisnummer = "56a",
                Straatnaam = "FrederickStraat",
                Plaats = "Breda"
            };

            _bestelling = new Bestelling
            {
                Artikelen = artikelNummers,
                Klant = klant
            };
        }

        [TestMethod]
        public void CorrectBestellingIsAccepted()
        {
            // Act
            var modelValidation = new ModelValidation(_bestelling);

            // Assert
            Assert.IsTrue(modelValidation.ValidationResultList.Count == 0);
        }

        [TestMethod]
        public void BestellingNoArtikelListRejected()
        {
            // Arrange
            _bestelling.Artikelen = null;

            // Act
            var modelValidation = new ModelValidation(_bestelling);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Geen artikelen geselecteerd"));
        }

        [TestMethod]
        public void BestellingEmptyArtikelListRejected()
        {
            // Arrange
            _bestelling.Artikelen = new List<int>();

            // Act
            var modelValidation = new ModelValidation(_bestelling);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Geen artikelen geselecteerd"));
        }

        [TestMethod]
        public void BestellingOneArtikelListAccepted()
        {
            // Arrange
            _bestelling.Artikelen = new List<int> { 6 };

            // Act
            var modelValidation = new ModelValidation(_bestelling);

            // Assert
            Assert.AreEqual(0, modelValidation.ValidationResultList.Count);
        }

        [TestMethod]
        public void BestellingNoKlantRejected()
        {
            // Arrange
            _bestelling.Klant = null;

            // Act
            var modelValidation = new ModelValidation(_bestelling);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Klant gegevens ontbreken"));
        }
    }
}
