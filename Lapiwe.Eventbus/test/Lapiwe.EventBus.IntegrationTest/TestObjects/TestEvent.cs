using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.WSA.EventBus.IntegrationTest
{
    internal class TestEvent : DomainEvent
    {
        public TestEvent(string routingKey = "")
        {
            RoutingKey = routingKey;
        }
    }
}
