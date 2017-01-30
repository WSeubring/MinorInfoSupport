using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.Audit.Test.TestObjects
{
    public class TestEvent : DomainEvent
    {
        public TestEvent(string routingKey = "")
        {
            RoutingKey = routingKey;
        }
    }
}
