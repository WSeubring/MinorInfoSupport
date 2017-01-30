using HAZ.FeBestellingen.Domain.Entities;
using HAZ.FeBestellingen.Domain.Infrastructure.Repositories;
using HAZ.FeBestellingen.Domain.Services;
using HAZ.FeBestellingen.Outgoing;
using InfoSupport.WSA.Common;
using InfoSupport.WSA.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Api.Test.Services
{
    [TestClass]
    public class BestellingServiceTest
    {
        [TestMethod]
        public void GetNextBestellingReturnsBestelling()
        {
            // Arrange
            var repositoryMock = new Mock<IBestellingRepository>(MockBehavior.Strict);
            repositoryMock.Setup(repository => repository.GetBestellingWithBestellingregels()).Returns(new Bestelling());
            repositoryMock.Setup(repository => repository.Update(It.IsAny<Bestelling>())).Returns(It.IsAny<int>());
            repositoryMock.Setup(repository => repository.GetBestellingWithBestellingregels()).Returns(new Bestelling());
            repositoryMock.Setup(repository => repository.Dispose());
            var eventPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            eventPublisher.Setup(publisher => publisher.Publish(It.IsAny<BestellingGepickedEvent>()));
            eventPublisher.Setup(publisher => publisher.Dispose());
            BestellingService service = new BestellingService(repositoryMock.Object, eventPublisher.Object);

            // Act
            var result = service.GetNextBestelling();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Bestelling));
        }

        [TestMethod]
        public void GetNextBestellingCallsRepository()
        {
            // Arrange
            var repositoryMock = new Mock<IBestellingRepository>(MockBehavior.Strict);
            repositoryMock.Setup(repository => repository.GetBestellingWithBestellingregels()).Returns(new Bestelling());
            repositoryMock.Setup(repository => repository.Update(It.IsAny<Bestelling>())).Returns(It.IsAny<int>());
            repositoryMock.Setup(repository => repository.GetBestellingWithBestellingregels()).Returns(new Bestelling());
            repositoryMock.Setup(repository => repository.Dispose());
            var eventPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            eventPublisher.Setup(publisher => publisher.Publish(It.IsAny<BestellingGepickedEvent>()));
            eventPublisher.Setup(publisher => publisher.Dispose());
            BestellingService service = new BestellingService(repositoryMock.Object, eventPublisher.Object);

            // Act
            var result = service.GetNextBestelling();

            // Assert
            repositoryMock.Verify(repository => repository.GetBestellingWithBestellingregels(), Times.Once);
        }

        [TestMethod]
        public void GetNextBestellingReturnsNullWhenAllBestellingenArePicked()
        {
            // Arrange
            var repositoryMock = new Mock<IBestellingRepository>(MockBehavior.Strict);
            repositoryMock.Setup(repository => repository.GetBestellingWithBestellingregels()).Returns( new Bestelling() { Bestelnummer = 1, BestelStatus = "picked" });
            repositoryMock.Setup(repository => repository.Update(It.IsAny<Bestelling>())).Returns(It.IsAny<int>());
            repositoryMock.Setup(repository => repository.GetBestellingWithBestellingregels()).Returns((Bestelling)null);
            repositoryMock.Setup(repository => repository.Dispose());
            var eventPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            eventPublisher.Setup(publisher => publisher.Publish(It.IsAny<BestellingGepickedEvent>()));
            eventPublisher.Setup(publisher => publisher.Dispose());
            BestellingService service = new BestellingService(repositoryMock.Object, eventPublisher.Object);

            // Act
            var result = service.GetNextBestelling();

            // Assert
            Assert.IsNull(result);
            Assert.IsInstanceOfType(result, typeof(Bestelling));
        }

        [TestMethod]
        public void PickBestellingCallsRepository()
        {
            // Arrange
            var repositoryMock = new Mock<IBestellingRepository>(MockBehavior.Strict);
            repositoryMock.Setup(repository => repository.FindAll()).Returns(new List<Bestelling>() { new Bestelling() { Bestelregels = new List<Bestelregel>() { new Bestelregel() { AantalArtikelen = 3, ArtikelId = 2 } } } });
            repositoryMock.Setup(repository => repository.GetBestellingByID(It.IsAny<int>())).Returns(new Bestelling() { Bestelregels = new List<Bestelregel>() { new Bestelregel() { AantalArtikelen = 3, ArtikelId = 2 } } });
            repositoryMock.Setup(repository => repository.Update(It.IsAny<Bestelling>())).Returns(It.IsAny<int>());
            repositoryMock.Setup(repository => repository.Dispose());
            var eventPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            eventPublisher.Setup(publisher => publisher.Publish(It.IsAny<BestellingGepickedEvent>()));
            eventPublisher.Setup(publisher => publisher.Dispose());
            BestellingService service = new BestellingService(repositoryMock.Object, eventPublisher.Object);

            int bestelnummer = 1;

            // Act
            service.PickBestelling(bestelnummer);

            // Assert
            repositoryMock.Verify(repository => repository.Update(It.IsAny<Bestelling>()), Times.Once);
        }

        [TestMethod]
        public void PickBestellingChangeBestellingStatusToPicked()
        {
            // Arrange
            var bestelling = new Bestelling() { Bestelregels = new List<Bestelregel>() { new Bestelregel() { AantalArtikelen = 3, ArtikelId = 2 } } };
            var repositoryMock = new Mock<IBestellingRepository>(MockBehavior.Strict);
            repositoryMock.Setup(repository => repository.FindAll()).Returns(new List<Bestelling>() { new Bestelling() { Bestelregels = new List<Bestelregel>() { new Bestelregel() { AantalArtikelen = 3, ArtikelId = 2 } } } });
            repositoryMock.Setup(repository => repository.GetBestellingByID(It.IsAny<int>())).Returns(bestelling);
            repositoryMock.Setup(repository => repository.Update(It.IsAny<Bestelling>())).Returns(It.IsAny<int>());
            repositoryMock.Setup(repository => repository.Dispose());
            var eventPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            eventPublisher.Setup(publisher => publisher.Publish(It.IsAny<BestellingGepickedEvent>()));
            eventPublisher.Setup(publisher => publisher.Dispose());
            BestellingService service = new BestellingService(repositoryMock.Object, eventPublisher.Object);

            int bestelnummer = 1;
            string expectedStatus = "picked";

            // Act
            service.PickBestelling(bestelnummer);

            // Assert
            Assert.AreEqual(bestelling.BestelStatus, expectedStatus);
        }

        [TestMethod]
        public void PickBestellingThrowsExceptionWhenBestellingIsNotFound()
        {
            // Arrange
            var repositoryMock = new Mock<IBestellingRepository>(MockBehavior.Strict);
            repositoryMock.Setup(repository => repository.FindAll()).Returns(new List<Bestelling>() { new Bestelling() });
            repositoryMock.Setup(repository => repository.GetBestellingByID(It.IsAny<int>())).Returns((Bestelling)null);
            repositoryMock.Setup(repository => repository.Update(It.IsAny<Bestelling>())).Returns(It.IsAny<int>());
            repositoryMock.Setup(repository => repository.Dispose());
            var eventPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            eventPublisher.Setup(publisher => publisher.Publish(It.IsAny<BestellingGepickedEvent>()));
            eventPublisher.Setup(publisher => publisher.Dispose());
            BestellingService service = new BestellingService(repositoryMock.Object, eventPublisher.Object);

            int bestelnummer = 1;

            //  Act / Assert
            Assert.ThrowsException<KeyNotFoundException>(() => service.PickBestelling(bestelnummer));
        }

        [TestMethod]
        public void PickBestellingThrowsEventAfterPicked()
        {
            // Arrange
            BestellingGepickedEvent thrownEvent = null;
            var repositoryMock = new Mock<IBestellingRepository>(MockBehavior.Strict);
            repositoryMock.Setup(repository => repository.FindAll()).Returns(new List<Bestelling>() { new Bestelling() { Bestelnummer = 1, BestelStatus = "picked", Bestelregels = new List<Bestelregel>() { new Bestelregel() { AantalArtikelen = 3, ArtikelId = 2 } } } });
            repositoryMock.Setup(repository => repository.GetBestellingByID(It.IsAny<int>())).Returns(new Bestelling() { Bestelnummer = 1, BestelStatus = "picked", Bestelregels = new List<Bestelregel>() { new Bestelregel() { AantalArtikelen = 3, ArtikelId = 2 } } });
            repositoryMock.Setup(repository => repository.Update(It.IsAny<Bestelling>())).Returns(It.IsAny<int>());
            repositoryMock.Setup(repository => repository.Dispose());
            var eventPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            eventPublisher.Setup(publisher => publisher.Publish(It.IsAny<BestellingGepickedEvent>())).Callback<DomainEvent>(parameter => thrownEvent = (parameter as BestellingGepickedEvent));
            eventPublisher.Setup(publisher => publisher.Dispose());
            BestellingService service = new BestellingService(repositoryMock.Object, eventPublisher.Object);

            int bestelnummer = 1;

            // Act
            service.PickBestelling(bestelnummer);

            // Assert
            eventPublisher.Verify(publisher => publisher.Publish(It.IsAny<BestellingGepickedEvent>()), Times.Once);
            Assert.AreEqual(1, thrownEvent.Artikelen.Count());
            Assert.AreEqual(2, thrownEvent.Artikelen.First().ArtikelID);
            Assert.AreEqual(3, thrownEvent.Artikelen.First().AantalGepicked);
        }

        [TestMethod]
        public void PickBestellingThrowsEventAfterPickedWithMultipleArtikelen()
        {
            // Arrange
            BestellingGepickedEvent thrownEvent = null;
            var repositoryMock = new Mock<IBestellingRepository>(MockBehavior.Strict);
            repositoryMock.Setup(repository => repository.FindAll()).Returns(new List<Bestelling>() { new Bestelling() { Bestelnummer = 1, BestelStatus = "picked", Bestelregels = new List<Bestelregel>() { new Bestelregel() { AantalArtikelen = 3, ArtikelId = 2 }, new Bestelregel() { AantalArtikelen = 5, ArtikelId = 3 } } } });
            repositoryMock.Setup(repository => repository.GetBestellingByID(It.IsAny<int>())).Returns(new Bestelling() { Bestelnummer = 1, BestelStatus = "picked", Bestelregels = new List<Bestelregel>() { new Bestelregel() { AantalArtikelen = 3, ArtikelId = 2 }, new Bestelregel() { AantalArtikelen = 5, ArtikelId = 3 } } });
            repositoryMock.Setup(repository => repository.Update(It.IsAny<Bestelling>())).Returns(It.IsAny<int>());
            repositoryMock.Setup(repository => repository.Dispose());
            var eventPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            eventPublisher.Setup(publisher => publisher.Publish(It.IsAny<BestellingGepickedEvent>())).Callback<DomainEvent>(parameter => thrownEvent = (parameter as BestellingGepickedEvent));
            eventPublisher.Setup(publisher => publisher.Dispose());
            BestellingService service = new BestellingService(repositoryMock.Object, eventPublisher.Object);

            int bestelnummer = 1;

            // Act
            service.PickBestelling(bestelnummer);

            // Assert
            eventPublisher.Verify(publisher => publisher.Publish(It.IsAny<BestellingGepickedEvent>()), Times.Once);
            Assert.AreEqual(2, thrownEvent.Artikelen.Count());
            CollectionAssert.AllItemsAreUnique(thrownEvent.Artikelen);
            CollectionAssert.AllItemsAreNotNull(thrownEvent.Artikelen);
            foreach (var item in thrownEvent.Artikelen)
            {
                if(item.ArtikelID == 2)
                {
                    Assert.AreEqual(3, item.AantalGepicked);
                }else if (item.ArtikelID == 3)
                {
                    Assert.AreEqual(5, item.AantalGepicked);
                } else
                {
                    Assert.Fail();
                }
            }
        }
    }
}
