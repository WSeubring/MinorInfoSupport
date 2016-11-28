using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Lapiwe.IS.RDW.Tests
{
    [TestClass]
    public class MappingTests
    {
        [TestMethod]
        public void TestXmlSerialization()
        {
            // Arrange
            keuringsverzoek result;
            using (var filestream = XmlReader.Create("XMLTestFiles/Keuringsverzoek.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(keuringsverzoek));

                // Act
                result = (keuringsverzoek)serializer.Deserialize(filestream);
            }

            // Assert
            Assert.AreEqual("f5ba58ff-7604-49d3-906d-26dedbe7cf25", result.correlatieId);
            Assert.AreEqual("A. Eigenaar", result.voertuig.naam);
            Assert.AreEqual(12345, result.voertuig.kilometerstand);

            Assert.AreEqual(new DateTime(2008, 11, 19), result.keuringsdatum);
            Assert.AreEqual("garage", result.keuringsinstantie.type);
            Assert.AreEqual("3013 5370", result.keuringsinstantie.kvk);
            Assert.AreEqual("Garage Voorbeeld B.V.", result.keuringsinstantie.naam);
            Assert.AreEqual("Wijk bij Voorbeeld", result.keuringsinstantie.plaats);
        }
    }
}
