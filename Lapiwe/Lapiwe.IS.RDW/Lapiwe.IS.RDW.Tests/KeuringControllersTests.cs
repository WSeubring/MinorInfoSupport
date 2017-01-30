using Lapiwe.Common.Infastructure;
using Lapiwe.IS.RDW.Agents;
using Lapiwe.IS.RDW.Agents.Interfaces;
using Lapiwe.IS.RDW.Controllers;
using Lapiwe.IS.RDW.DAL;
using Lapiwe.IS.RDW.Export.Commands;
using Lapiwe.IS.RDW.Export.Events;
using Lapiwe.IS.RDW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

            KeuringsVerzoekCommand keuringsVerzoekCommand = DefaultKeuringsVerzoekCommand();
            string responseXml = File.ReadAllText("XMLTestFiles/KeuringsRegistratieZonderSteefproef.xml");

            var mockContext = new Mock<LogContext>();
            mockContext.Setup(context => context.Logs.Add(It.IsAny<Log>()));
            mockContext.Setup(context => context.SaveChanges());


            var mockRDWAgent = new Mock<IRDWAgent>(MockBehavior.Strict);
            mockRDWAgent.Setup(rdwAgent => rdwAgent.SendKeuringsVerzoekAsync(It.IsAny<string>()))
                .ReturnsAsync(responseXml);

            var target = new KeuringController(mockContext.Object, mockPublisher.Object, mockRDWAgent.Object);

            // Act
            var result = target.Verzoek(keuringsVerzoekCommand).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            mockPublisher.Verify(publisher => publisher.Publish(It.Is<KeuringVerwerktZonderSteekproefEvent>(e =>
                                                                e.OnderhoudsGuid == Guid.ParseExact("c4ab88e8-b266-4816-a174-d4cf26b3832b", "D")))
                                                                , Times.Once);

            mockContext.Verify(context => context.Logs.Add(It.Is<Log>(log => log.Xml == responseXml)), Times.Once);
            mockContext.Verify(context => context.Logs.Add(It.IsAny<Log>()), Times.Exactly(2));
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void MeldKeuringMetSteekproef()
        {
            // Arrange
            var mockPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);
            mockPublisher.Setup(publisher => publisher.Publish(It.IsAny<KeuringVerwerktMetSteekproefEvent>()));

            KeuringsVerzoekCommand keuringsVerzoekCommand = DefaultKeuringsVerzoekCommand();
            string responseXml = File.ReadAllText("XMLTestFiles/KeuringsRegistratieMetSteefproef.xml");

            var mockContext = new Mock<LogContext>();
            mockContext.Setup(context => context.Logs.Add(It.IsAny<Log>()));
            mockContext.Setup(context => context.SaveChanges());

            var mockRDWAgent = new Mock<IRDWAgent>(MockBehavior.Strict);
            mockRDWAgent.Setup(rdwAgent => rdwAgent.SendKeuringsVerzoekAsync(It.IsAny<string>()))
                .ReturnsAsync(responseXml);

            var target = new KeuringController(mockContext.Object, mockPublisher.Object, mockRDWAgent.Object);

            // Act
            var result = target.Verzoek(keuringsVerzoekCommand).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            mockPublisher.Verify(publisher => publisher.Publish(It.Is<KeuringVerwerktMetSteekproefEvent>(e =>
                                                                e.OnderhoudsGuid == Guid.ParseExact("c4ab88e8-b266-4816-a174-d4cf26b3832b", "D")))
                                                                , Times.Once);

            mockContext.Verify(context => context.Logs.Add(It.IsAny<Log>()),Times.Exactly(2));
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void BadRequestKeuringverzoek()
        {
            var mockPublisher = new Mock<IEventPublisher>(MockBehavior.Strict);

            KeuringsVerzoekCommand keuringsVerzoekCommand = DefaultKeuringsVerzoekCommand();

            var mockContext = new Mock<LogContext>();

            var mockRDWAgent = new Mock<IRDWAgent>(MockBehavior.Strict);
            mockRDWAgent.Setup(rdwAgent => rdwAgent.SendKeuringsVerzoekAsync(It.IsAny<string>()))
                .Throws<AggregateException>();

            var target = new KeuringController(mockContext.Object, mockPublisher.Object, mockRDWAgent.Object);

            // Act
            var result = target.Verzoek(keuringsVerzoekCommand).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            mockContext.Verify(context => context.Logs.Add(It.IsAny<Log>()), Times.Never);
        }

        private KeuringsVerzoekCommand DefaultKeuringsVerzoekCommand()
        {
            return new KeuringsVerzoekCommand()
            {
                OnderhoudsGuid = Guid.ParseExact("c4ab88e8-b266-4816-a174-d4cf26b3832b", "D"),
                Kenteken = "12-23-dd",
                Kilometerstand = 1234,
                Klantnaam = "Harry de lois"
            };
        }
    }
}
