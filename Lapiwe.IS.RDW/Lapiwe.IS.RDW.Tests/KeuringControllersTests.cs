using Lapiwe.Common.Infastructure;
using Lapiwe.IS.RDW.Agents;
using Lapiwe.IS.RDW.Controllers;
using Lapiwe.IS.RDW.DAL;
using Lapiwe.IS.RDW.Export.Commands;
using Lapiwe.IS.RDW.Export.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.IS.RDW.Tests
{
    [TestClass]
    public class KeuringControllersTests
    {
        [TestMethod]
        public void MeldKeuringZonderSteekproef()
        {
            // Arrange
            var mockPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            mockPublisher.Setup(publisher => publisher.Publish(It.IsAny<KeuringVerwerktZonderSteekproefEvent>()));

            var verzoek = new keuringsverzoek()
            {

            };

            var mockContext = new Mock<LogContext>(MockBehavior.Strict);
            mockContext.Setup(context => context.Logs.Add(It.IsAny<keuringsverzoek>()));
            mockContext.Setup(context => context.SaveChanges());

            var mockRDWAgent = new Mock<RDWAgent>(MockBehavior.Strict);
            mockRDWAgent.Setup(rdwAgent => rdwAgent.SendKeuringsVerzoek(It.IsAny<keuringsverzoek>()))
                .Returns(verzoek);

            var target = new KeuringController(mockContext.Object, mockPublisher.Object);

            var keuringsVerzoekCommand = new KeuringsVerzoekCommand()
            {
                Kenteken = "12-23-d3",
                Kilometerstand = 1234,
                Klantnaam = "Harry de lois"
            };
            // Act
            var result = target.KeuringsVerzoek(keuringsVerzoekCommand);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            mockPublisher.Verify(publisher => publisher.Publish(new KeuringVerwerktZonderSteekproefEvent()
            {
                Kenteken = "12-23-d3"
            }), Times.Once);

            mockContext.Verify(context => context.Logs.Add(verzoek), Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);

        }
    }
}
