using System;
using Lapiwe.EventBus.Dispatchers;
using Lapiwe.EventBus.Domain;

namespace Minor.WSA.EventBus.IntegrationTest
{
    internal class TestDispatcherWithoutOtherEvent : EventDispatcher
    {

        public TestDispatcherWithoutOtherEvent(BusOptions options = null) : base (options)
        {
        }

        public int receivedTestEventCount { get; set; }

        public void TestSent(TestEvent e)
        {
            receivedTestEventCount++;
        }
    }
}