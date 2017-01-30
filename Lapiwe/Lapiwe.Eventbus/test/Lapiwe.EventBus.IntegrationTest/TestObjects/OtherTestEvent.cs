using Lapiwe.Common.Domain;

namespace Minor.WSA.EventBus.IntegrationTest
{
    internal class OtherTestEvent : DomainEvent
    {
        public OtherTestEvent(string routingKey = "Test.Event")
        {
            RoutingKey = routingKey;
        }
    }
}