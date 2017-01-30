using Lapiwe.EventBus.Common;
using Lapiwe.EventBus.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.WSA.EventBus.IntegrationTest
{
    [TestClass]
    public class DispatcherTest
    {
        [TestMethod]
        public void Dispatcher_DuplicateMethodWithParameterName()
        {

            // Arrange
            var options = new BusOptions { ExchangeName = "TestExchange1", HostName = "Localhost", Port = 5672, Username = "guest", Password = "guest" };

            Action act = () => new TestDispatcherWithDuplicateParameters(options);

            Assert.ThrowsException<DuplicateMethodWithSameEventParameterException>(act);
        }
    }
}
