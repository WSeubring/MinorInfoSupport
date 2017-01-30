using HAZ.FeBestellingen.Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HAZ.FeBestellingen.Domain.Services;
using HAZ.FeBestellingen.Domain.Entities;
using HAZ.FeBestellingen.Api.Models.BestellingViewModels;
using System;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using HAZ.FeBestellingen.Domain.Infrastructure.Agents;
using Kantilever.Magazijnbeheer;
using Kantilever.Magazijnbeheer.Commands;

namespace HAZ.FeBestellingen.Api.Test
{
    [TestClass]
    public class IntranetTest
    {
        [TestMethod]
        public void IndexReturnsIActionResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IntranetController>>();
            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            bestellingServiceMock.Setup(service => service.GetNextBestelling()).Returns(new Bestelling() { Bestelnummer = 1, Bestelregels = new List<Bestelregel> { new Bestelregel() } });
            bestellingServiceMock.Setup(service => service.PickBestelling(It.IsAny<int>()));
            bestellingServiceMock.Setup(service => service.Dispose());

            var controller = new IntranetController(loggerMock.Object, bestellingServiceMock.Object, magazijnBeheerAgent.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]
        public void IndexCallsBestellingenService()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IntranetController>>();
            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            bestellingServiceMock.Setup(service => service.GetNextBestelling()).Returns(new Bestelling() { Bestelnummer = 1, Bestelregels = new List<Bestelregel> { new Bestelregel() } });
            bestellingServiceMock.Setup(service => service.PickBestelling(It.IsAny<int>()));
            bestellingServiceMock.Setup(service => service.Dispose());

            var controller = new IntranetController(loggerMock.Object, bestellingServiceMock.Object, magazijnBeheerAgent.Object);

            // Act
            var result = controller.Index();

            // Assert
            bestellingServiceMock.Verify(service => service.GetNextBestelling(), Times.Once());
        }

        public void IndexReturnsListWithBestellingRegelViewModels()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IntranetController>>();
            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            bestellingServiceMock.Setup(service => service.GetNextBestelling()).Returns(new Bestelling() { Bestelnummer = 1, Bestelregels = new List<Bestelregel> { new Bestelregel() } });
            bestellingServiceMock.Setup(service => service.PickBestelling(It.IsAny<int>()));
            bestellingServiceMock.Setup(service => service.Dispose());

            var controller = new IntranetController(loggerMock.Object, bestellingServiceMock.Object, magazijnBeheerAgent.Object);

            // Act
            var result = controller.Index();

            // Assert
            ViewResult viewResult = (ViewResult)result;
            var model = viewResult.Model;
            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(List<BestellingRegelViewModel>));
        }

        [TestMethod]
        public void IndexReturnsListWithBestellingRegelViewModelsWithMatchingBestelregel()
        {
            // Arrange
            var expectedBestellingRegelViewModel = new BestellingRegelViewModel { Naam = "Fietsbel Zwart", Aantal = 2, AfbeeldingUrl = "fietsbel_zwart.jpg", LeveranciersCode = "198-DI" };
            var expectedBestellingViewModel = new BestellingViewModel() { Bestelnummer = 1 };
            expectedBestellingViewModel.BestellingRegelViewModels.Add(expectedBestellingRegelViewModel);

            var loggerMock = new Mock<ILogger<IntranetController>>();
            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            bestellingServiceMock.Setup(service => service.Dispose());

            // add fake bestelregel (entity)
            var bestelregel = new Bestelregel() { BestelregelId = 1, AantalArtikelen = 2, ArtikelBeschrijving = "Fietsbel zwart", ArtikelId = 1, ArtikelNaam = "Fietsbel Zwart", PrijsPerArtikelInc = 10, PrijsPerArtikelExc = 9, AfbeeldingUrl = "fietsbel_zwart.jpg", LeverancierCode = "198-DI" };

            bestellingServiceMock.Setup(service => service.GetNextBestelling()).Returns(new Bestelling() { Bestelnummer = 1, Bestelregels = new List<Bestelregel> { bestelregel } });

            var controller = new IntranetController(loggerMock.Object, bestellingServiceMock.Object, magazijnBeheerAgent.Object);


            // Act
            var result = controller.Index();

            // Assert
            var viewResult = (ViewResult)result;
            var model = viewResult.Model;
            Assert.IsNotNull(model);
            var bestellingViewModel = (BestellingViewModel)model;
            var bestelRegelViewModel = bestellingViewModel.BestellingRegelViewModels.FirstOrDefault();
            Assert.AreEqual(expectedBestellingViewModel.Bestelnummer, bestellingViewModel.Bestelnummer);
            Assert.IsNotNull(bestelRegelViewModel);
            Assert.AreEqual(expectedBestellingRegelViewModel.Naam, bestelRegelViewModel.Naam);
            Assert.AreEqual(expectedBestellingRegelViewModel.Aantal, bestelRegelViewModel.Aantal);
            Assert.AreEqual(expectedBestellingRegelViewModel.AfbeeldingUrl, bestelRegelViewModel.AfbeeldingUrl);
            Assert.AreEqual(expectedBestellingRegelViewModel.LeveranciersCode, bestelRegelViewModel.LeveranciersCode);
        }

        [TestMethod]
        public void PickBestellingAndContinueRedirectsToIndex()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IntranetController>>();
            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            magazijnBeheerAgent.Setup(agent => agent.SendHaalArtikelUitMagazijnCommand(It.IsAny<HaalArtikelUitMagazijnCommand>()));
            bestellingServiceMock.Setup(service => service.GetBestelling(It.IsAny<int>())).Returns(new Bestelling() { Bestelnummer = 1, Bestelregels = new List<Bestelregel> { new Bestelregel() { ArtikelId = 1, AantalArtikelen = 1 } } });
            bestellingServiceMock.Setup(service => service.GetNextBestelling()).Returns(new Bestelling() { Bestelnummer = 1, Bestelregels = new List<Bestelregel> { new Bestelregel() } });
            bestellingServiceMock.Setup(service => service.PickBestelling(It.IsAny<int>()));
            bestellingServiceMock.Setup(service => service.Dispose());

            var controller = new IntranetController(loggerMock.Object, bestellingServiceMock.Object, magazijnBeheerAgent.Object);

            int bestelnummer = 1;

            // Act
            ViewResult result = (ViewResult) controller.PickBestellingAndContinue(bestelnummer);

            // Assert
            Assert.AreEqual("Index", result.ViewName);
            magazijnBeheerAgent.Verify(agent => agent.SendHaalArtikelUitMagazijnCommand(It.IsAny<HaalArtikelUitMagazijnCommand>()), Times.Once);

        }

        [TestMethod]
        public void PickBestellingAndContinueCallsBestellingService()
        {
            // Arrange
            int pickBestellingParameter = 0;

            var loggerMock = new Mock<ILogger<IntranetController>>();
            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            magazijnBeheerAgent.Setup(agent => agent.SendHaalArtikelUitMagazijnCommand(It.IsAny<HaalArtikelUitMagazijnCommand>()));
            bestellingServiceMock.Setup(service => service.GetNextBestelling()).Returns(new Bestelling() { Bestelnummer = 1, Bestelregels = new List<Bestelregel> { new Bestelregel() } });
            bestellingServiceMock.Setup(service => service.GetBestelling(It.IsAny<int>())).Returns(new Bestelling() { Bestelnummer = 1, Bestelregels = new List<Bestelregel> { new Bestelregel() { ArtikelId = 1, AantalArtikelen = 1 } } });
            bestellingServiceMock.Setup(service => service.PickBestelling(It.IsAny<int>())).Callback<int>(parameter => pickBestellingParameter = parameter);
            bestellingServiceMock.Setup(service => service.Dispose());

            var controller = new IntranetController(loggerMock.Object, bestellingServiceMock.Object, magazijnBeheerAgent.Object);
            int bestelnummer = 1;

            // Act
            var result = controller.PickBestellingAndContinue(bestelnummer);

            // Assert
            Assert.AreEqual(bestelnummer, pickBestellingParameter);
            bestellingServiceMock.Verify(service => service.PickBestelling(It.IsAny<int>()), Times.Once());
            magazijnBeheerAgent.Verify(agent => agent.SendHaalArtikelUitMagazijnCommand(It.IsAny<HaalArtikelUitMagazijnCommand>()), Times.Once);

        }

        [TestMethod]
        public void PickBestellingAndContinueRedirectsToPauseIfNoBestellingen()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IntranetController>>();
            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            magazijnBeheerAgent.Setup(agent => agent.SendHaalArtikelUitMagazijnCommand(It.IsAny<HaalArtikelUitMagazijnCommand>()));
            bestellingServiceMock.Setup(service => service.GetBestelling(It.IsAny<int>())).Returns(new Bestelling() { Bestelnummer = 1, Bestelregels = new List<Bestelregel> { new Bestelregel() { ArtikelId = 1, AantalArtikelen = 1 } } });
            bestellingServiceMock.Setup(service => service.GetNextBestelling()).Returns((Bestelling)null);
            bestellingServiceMock.Setup(service => service.PickBestelling(It.IsAny<int>()));
            bestellingServiceMock.Setup(service => service.Dispose());
            var controller = new IntranetController(loggerMock.Object, bestellingServiceMock.Object, magazijnBeheerAgent.Object);

            int bestelnummer = 1;

            // Act
            ViewResult result = (ViewResult)controller.PickBestellingAndContinue(bestelnummer);

            // Assert
            Assert.AreEqual("Pause", result.ViewName);
            magazijnBeheerAgent.Verify(agent => agent.SendHaalArtikelUitMagazijnCommand(It.IsAny<HaalArtikelUitMagazijnCommand>()),Times.Once);
        }

        [TestMethod]
        public void PickBestellingAndPauseRedirectsToPause()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IntranetController>>();
            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            bestellingServiceMock.Setup(service => service.GetNextBestelling()).Returns(new Bestelling() { Bestelnummer = 1, Bestelregels = new List<Bestelregel> { new Bestelregel() } });
            bestellingServiceMock.Setup(service => service.PickBestelling(It.IsAny<int>()));
            bestellingServiceMock.Setup(service => service.Dispose());

            var controller = new IntranetController(loggerMock.Object, bestellingServiceMock.Object, magazijnBeheerAgent.Object);

            int bestelnummer = 1;

            // Act
            ViewResult result = (ViewResult) controller.PickBestellingAndPause(bestelnummer);

            // Assert
            Assert.AreEqual("Pause", result.ViewName);

        }

        [TestMethod]
        public void PickBestellingAndPauseCallsBestellingService()
        {
            // Arrange
            int pickBestellingParameter = 0;

            var loggerMock = new Mock<ILogger<IntranetController>>();
            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            bestellingServiceMock.Setup(service => service.GetNextBestelling()).Returns(new Bestelling() { Bestelnummer = 1, Bestelregels = new List<Bestelregel> { new Bestelregel() } });
            bestellingServiceMock.Setup(service => service.PickBestelling(It.IsAny<int>())).Callback<int>(parameter => pickBestellingParameter = parameter);
            bestellingServiceMock.Setup(service => service.Dispose());

            var controller = new IntranetController(loggerMock.Object, bestellingServiceMock.Object, magazijnBeheerAgent.Object);

            int bestelnummer = 1;

            // Act
            var result = controller.PickBestellingAndPause(bestelnummer);

            // Assert
            Assert.AreEqual(bestelnummer, pickBestellingParameter);
            bestellingServiceMock.Verify(service => service.PickBestelling(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        public void IndexUnexpextedErrorOccuredTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IntranetController>>();
            var bestellingServiceMock = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            bestellingServiceMock.Setup(service => service.GetNextBestelling()).Throws<Exception>();

            var controller = new IntranetController(loggerMock.Object, bestellingServiceMock.Object, magazijnBeheerAgent.Object);

            // Act
            var result = controller.Index();

            // Assert
            var view = (result as ViewResult);
            Assert.AreEqual("Oeps", view.ViewName);
        }

        [TestMethod]
        public void FactuurUnexpextedErrorOccuredTest()
        {
            // Arrange
            var logger = new Mock<ILogger<IntranetController>>();
            var bestellingService = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            bestellingService.Setup(service => service.GetBestelling(It.IsAny<int>())).Throws<Exception>();

            var controller = new IntranetController(logger.Object, bestellingService.Object, magazijnBeheerAgent.Object);

            // Act
            var result = controller.Factuur(1);

            // Assert
            var view = (result as ViewResult);
            Assert.AreEqual("Oeps", view.ViewName);
        }

        [TestMethod]
        public void PickBestellingAndContinueUnexpextedErrorOccuredTest()
        {
            // Arrange
            var logger = new Mock<ILogger<IntranetController>>();
            var bestellingService = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            bestellingService.Setup(service => service.PickBestelling(It.IsAny<int>())).Throws<Exception>();

            var controller = new IntranetController(logger.Object, bestellingService.Object, magazijnBeheerAgent.Object);


            // Act
            var result = controller.PickBestellingAndContinue(1);

            // Assert
            var view = (result as ViewResult);
            Assert.AreEqual("Oeps", view.ViewName);
        }
        [TestMethod]
        public void PickBestellingAndPauseUnexpextedErrorOccuredTest()
        {
            // Arrange
            var logger = new Mock<ILogger<IntranetController>>();
            var bestellingService = new Mock<IBestellingService>(MockBehavior.Strict);
            var magazijnBeheerAgent = new Mock<IMagazijnBeheerAgent>(MockBehavior.Strict);

            bestellingService.Setup(service => service.PickBestelling(It.IsAny<int>())).Throws<Exception>();

            var controller = new IntranetController(logger.Object, bestellingService.Object, magazijnBeheerAgent.Object);

            // Act
            var result = controller.PickBestellingAndPause(1);

            // Assert
            var view = (result as ViewResult);
            Assert.AreEqual("Oeps", view.ViewName);
        }
    } 
}