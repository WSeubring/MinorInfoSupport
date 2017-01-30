using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using Lapiwe.EventBus.Dispatchers;
using Lapiwe.EventBus.Domain;

namespace Lapiwe.Audit.Test.TestObjects
{
    public class TestDispatcherImplEventDispatcher : EventDispatcher
    {
        public string eventType { get; private set; }
        public TestDispatcherImplEventDispatcher(BusOptions options = null, string queueName = null) : base(options, queueName)
        {

        }

        public int ReceivedTestEventCount { get; private set; }

        public void TestSent(TestEvent e)
        {
            ReceivedTestEventCount++;
        }
    }
}
