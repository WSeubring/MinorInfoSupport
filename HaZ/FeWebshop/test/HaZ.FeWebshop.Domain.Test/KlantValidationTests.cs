using HaZ.FeWebshop.Domain.Test.Utils;
using HAZ.FeWebshop.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace HaZ.FeWebshop.Domain.Test
{
    [TestClass]
    public class KlantValidationTests
    {
        private Klant _klant;

        [TestInitialize]
        public void InitializeCorrectKlant()
        {
            // Arrange
            _klant = new Klant {
                Naam = "Hans Worst",
                Land = "Nederland",
                Postcode = "4385ID",
                Huisnummer = "56a",
                Straatnaam = "FrederickStraat",
                Plaats = "Breda"
            };
        }

        [TestMethod]
        public void CorrectKlantIsAccepted()
        {
            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.IsTrue(modelValidation.ValidationResultList.Count == 0);
        }

        [TestMethod]
        public void KlantNoNaamRejected()
        {
            // Arrange
            _klant.Naam = "";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Klant naam ontbreekt"));
        }

        [TestMethod]
        public void KlantEmptyNaamRejected()
        {
            // Arrange
            _klant.Naam = "";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Klant naam ontbreekt"));
        }

        [TestMethod]
        public void KlantNoLandRejected()
        {
            // Arrange
            _klant.Land = "";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Land ontbreekt"));
        }

        [TestMethod]
        public void KlantNoPostcodeRejected()
        {
            // Arrange
            _klant.Postcode = "";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Postcode ontbreekt"));
        }

        [TestMethod]
        public void KlantPostcodeStartsWith0Rejected()
        {
            // Arrange
            _klant.Postcode = "0385ID";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Ongeldige Postcode"));
        }

        [TestMethod]
        public void KlantPostcodeContainsLetterOnWrongPositionRejected()
        {
            // Arrange
            _klant.Postcode = "43A5ID";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Ongeldige Postcode"));
        }

        [TestMethod]
        public void KlantPostcodeSeperatedWithSpaceAccepted()
        {
            // Arrange
            _klant.Postcode = "4385 ID";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(0, modelValidation.ValidationResultList.Count);
        }

        [TestMethod]
        public void KlantPostcodeContainsLowerCaseAccepted()
        {
            // Arrange
            _klant.Postcode = "4385dD";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(0, modelValidation.ValidationResultList.Count);
        }

        [TestMethod]
        public void KlantPostcodeBelgischePostcode()
        {
            // Arrange
            _klant.Postcode = "4385";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(0, modelValidation.ValidationResultList.Count);
        }

        [TestMethod]
        public void KlantPostcodeBelgischePostcodeStartWith0()
        {
            // Arrange
            _klant.Postcode = "0385";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Ongeldige Postcode"));
        }

        [TestMethod]
        public void KlantPostcodeBelgischePostcodeLongerThen4()
        {
            // Arrange
            _klant.Postcode = "13855";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Ongeldige Postcode"));
        }

        [TestMethod]
        public void KlantNoHuisnummerRejected()
        {
            // Arrange
            _klant.Huisnummer = "";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Huisnummer ontbreekt"));
        }

        [TestMethod]
        public void KlantNoStraatnaamRejected()
        {
            // Arrange
            _klant.Straatnaam = "";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Straatnaam ontbreekt"));
        }

        [TestMethod]
        public void KlantNoPlaatsRejected()
        {
            // Arrange
            _klant.Plaats = "";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(1, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Plaats ontbreekt"));
        }

        [TestMethod]
        public void KlantNoPlaatsAndWrongPostcodeProducesCorrectErrorMessages()
        {
            // Arrange
            _klant.Plaats = "";
            _klant.Postcode = "a123AB";

            // Act
            var modelValidation = new ModelValidation(_klant);

            // Assert
            Assert.AreEqual(2, modelValidation.ValidationResultList.Count);
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Plaats ontbreekt"));
            Assert.IsTrue(modelValidation.ContainsErrorMessage("Ongeldige Postcode"));
        }
    }
}
