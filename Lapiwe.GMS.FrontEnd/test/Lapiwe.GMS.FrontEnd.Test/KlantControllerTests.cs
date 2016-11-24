using Lapiwe.GMS.FrontEnd.Controllers;
using Lapiwe.GMS.FrontEnd.DAL.Interfaces;
using Lapiwe.GMS.FrontEnd.Enitities;
using Lapiwe.GMS.FrontEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lapiwe.GMS.FrontEnd.Test
{
    [TestClass]
    public class KlantControllerTests
    {
        [TestMethod]
        public void KlantDetailsWithExistingKlant()
        {
            // Arrange
            var klant = DefaultKlant();

            var mock = new Mock<IKlantRepository>(MockBehavior.Strict);
            mock.Setup(klantRepository => klantRepository.Find(1)).Returns(klant);

            var target = new KlantController(mock.Object);

            // Act
            var result = target.KlantDetails(1);

            // Assert
            var viewmodel = ((result as ViewResult).Model as KlantGegegevensViewModel);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(KlantGegegevensViewModel));
            Assert.AreEqual(klant, viewmodel.Klant);
        }

        private Klant DefaultKlant()
        {
            return new Klant()
            {
                ID = 1,
                Voornaam = "VoornaamTest",
                Tussenvoegsel = "de",
                Achternaam = "AchternaamTest",
                Emailadres = "emailadress@mail.com",
                Huisnummer = "12",
                Postcode = "1234AB",
                Straatnaam = "StraatPlein",
                Telefoonnummer = "0612345678",
                Woonplaats = "Dropplaats"
            };
        }
    }
}
