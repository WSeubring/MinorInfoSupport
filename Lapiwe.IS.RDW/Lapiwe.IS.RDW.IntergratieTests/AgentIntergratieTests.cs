using Lapiwe.IS.RDW.Agents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.IS.RDW.IntergratieTests
{
    [TestClass]
    public class AgentIntergratieTests
    {
        [TestMethod]
        public void RdwRequest()
        {
            var result = new RDWAgent().SendKeuringsVerzoekAsync(new keuringsverzoek()
            {
                keuringsdatum = new DateTime(2016,11,11),
                voertuig = new keuringsverzoekVoertuig()
                {
                    kenteken = "12-34-as",
                    kilometerstand = 10,
                    naam = "Henk",
                    type = voertuigtype.personenauto
                },
                keuringsinstantie = new keuringsinstantie()
                {
                    kvk = "3013 5370",
                    naam = "Garage Voorbeeld B.V.",
                    plaats = "Wijk bij Voorbeeld",
                    type = "garage"
                }
            }).Result;

            Assert.AreEqual(new DateTime(2016, 11, 11), result.keuringsdatum);
            Assert.AreEqual("12-34-as", result.kenteken);
        }
    }
}
