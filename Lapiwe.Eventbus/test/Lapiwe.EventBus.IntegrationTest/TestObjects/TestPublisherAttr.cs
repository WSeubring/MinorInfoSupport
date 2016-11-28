using Lapiwe.EventBus.Domain;
using Lapiwe.EventBus.Publishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.WSA.EventBus.IntegrationTest
{
    [BusOptions(ExchangeName = "TestExchange7")]
    internal class TestPublisherAttr : EventPublisher
    {
        public TestPublisherAttr(BusOptions options = null) : base(options)
        {
        }
    }
}
