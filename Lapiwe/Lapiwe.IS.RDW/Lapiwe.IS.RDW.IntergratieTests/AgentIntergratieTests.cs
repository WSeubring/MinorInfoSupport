using Lapiwe.IS.RDW.Agents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lapiwe.IS.RDW.IntergratieTests
{
    [TestClass]
    public class AgentIntergratieTests
    {
        [TestMethod]
        public void RdwRequest()
        {
            // Arrange
            var verzoek = File.ReadAllText("XMLTestFiles/Correct_KeuringsVerzoek.xml");
            var target = new RDWAgent();
            //Act
            var result = target.SendKeuringsVerzoekAsync(verzoek).Result;

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void BadRequestTest()
        {
            // Arrange
            var verzoek = File.ReadAllText("XMLTestFiles/Incorrect_KeuringsVerzoek.xml");
            var target = new RDWAgent();
           
            try
            {
                var result = target.SendKeuringsVerzoekAsync(verzoek).Result;

                Assert.Fail();
            }
            catch(Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(AggregateException));
            }
        }
    }
}
