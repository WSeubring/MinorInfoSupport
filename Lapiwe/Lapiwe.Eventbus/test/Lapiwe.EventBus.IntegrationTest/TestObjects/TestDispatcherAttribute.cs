using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using Lapiwe.EventBus.Dispatchers;
using Lapiwe.EventBus.Attributes;
using Lapiwe.EventBus.Domain;

namespace Minor.WSA.EventBus.IntegrationTest
{
    [RoutingKey("Test.Routing")]
    internal class TestDispatcherAttribute : EventDispatcher
    {

        public TestDispatcherAttribute(BusOptions options = null) : base (options)
        {

        }

        public int receivedTestEventCount { get; set; }
        public int receivedOtherTestEventCount { get; set; }
        public int receivedTotalTestCount { get; set; }

        public void TestSent(TestEvent e)
        {
            receivedTestEventCount++;
            receivedTotalTestCount++;
        }
    }
}
