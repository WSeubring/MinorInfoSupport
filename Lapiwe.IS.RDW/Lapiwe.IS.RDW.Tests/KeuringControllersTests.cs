using Lapiwe.Common.Infastructure;
using Lapiwe.IS.RDW.Agents;
using Lapiwe.IS.RDW.Agents.Interfaces;
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

            var keuringsVerzoekCommand = new KeuringsVerzoekCommand()
            {
                OnderhoudsGuid = Guid.ParseExact("c4ab88e8-b266-4816-a174-d4cf26b3832b", "D"),
                Kenteken = "12-23-dd",
                Kilometerstand = 1234,
                Klantnaam = "Harry de lois"
            };

            var response = new keuringsregistratie
            {
                steekproefSpecified = false,
                kenteken = "12-23-dd",
            };

            var verzoek = new keuringsverzoek
            {
                voertuig = new keuringsverzoekVoertuig
                {
                    kenteken = "12-23-dd",
                    kilometerstand = 1234,
                    naam = "Harry de lois"
                }
            };
            var mockContext = new Mock<LogContext>();
            mockContext.Setup(context => context.KeuringsVerzoeken.Add(It.IsAny<keuringsverzoek>()));
            mockContext.Setup(context => context.KeuringsRegistraties.Add(It.IsAny<keuringsregistratie>()));
            mockContext.Setup(context => context.SaveChanges());

            var mockRDWAgent = new Mock<IRDWAgent>(MockBehavior.Strict);
            mockRDWAgent.Setup(rdwAgent => rdwAgent.SendKeuringsVerzoekAsync(It.IsAny<keuringsverzoek>()))
                .ReturnsAsync(response);

            var target = new KeuringController(mockContext.Object, mockPublisher.Object, mockRDWAgent.Object);

            // Act
            var result = target.Verzoek(keuringsVerzoekCommand).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            mockPublisher.Verify(publisher => publisher.Publish(It.Is<KeuringVerwerktZonderSteekproefEvent>(e =>
                                                                e.OnderhoudsGuid == Guid.ParseExact("c4ab88e8-b266-4816-a174-d4cf26b3832b", "D")))
                                                                ,Times.Once);

            mockContext.Verify(context => context.KeuringsRegistraties.Add(response), Times.Once);
            mockContext.Verify(context => context.KeuringsVerzoeken.Add(It.Is<keuringsverzoek>(keuringsverzoek => 
                                                                                               keuringsverzoek.voertuig.kenteken == verzoek.voertuig.kenteken &&
                                                                                               keuringsverzoek.voertuig.kilometerstand == verzoek.voertuig.kilometerstand &&
                                                                                               keuringsverzoek.voertuig.naam == verzoek.voertuig.naam))
                                                                                               , Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);

        }
    }
}
