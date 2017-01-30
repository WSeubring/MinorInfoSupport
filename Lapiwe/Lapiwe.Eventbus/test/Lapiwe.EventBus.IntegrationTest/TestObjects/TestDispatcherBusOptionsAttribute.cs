using Lapiwe.EventBus.Dispatchers;
using Lapiwe.EventBus.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.WSA.EventBus.IntegrationTest
{
    [BusOptions(ExchangeName = "TestExchange7")]
    internal class TestDispatcherBusOptionsAttribute : EventDispatcher
    {

        public TestDispatcherBusOptionsAttribute(BusOptions options) : base(options)
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

        public void OtherTestSent(OtherTestEvent e)
        {
            receivedOtherTestEventCount++;
            receivedTotalTestCount++;
        }
    }
}
