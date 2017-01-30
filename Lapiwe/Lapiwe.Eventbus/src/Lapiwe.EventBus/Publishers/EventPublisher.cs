using System;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Reflection;
using System.Linq;
using Lapiwe.EventBus.Publishers;
using Lapiwe.EventBus.Common;
using Lapiwe.EventBus.Domain;
using Lapiwe.Common.Domain;
using Lapiwe.Common.Infastructure;

namespace Lapiwe.EventBus.Publishers
{
    public class EventPublisher : IEventPublisher
    {
        private Channel _channel;

        public EventPublisher(BusOptions options = null)
        {
            var type = this.GetType();
            var attributeOptions = type.GetTypeInfo().GetCustomAttributes<BusOptions>().FirstOrDefault();
            var busOptions = options ?? attributeOptions ?? new BusOptions();
            _channel = new Channel(busOptions);
            _channel.DeclareExchange();
        }

        public void Publish(DomainEvent domainEvent)
        {
            var json = JsonConvert.SerializeObject(domainEvent);
            var body = Encoding.UTF8.GetBytes(json);
            var eventType = domainEvent.GetType().FullName;
            _channel.Publish(routingKey: domainEvent.RoutingKey, body: body, eventType: eventType);
        }

        public void Dispose()
        {
            _channel?.Dispose();
        }
    }
}