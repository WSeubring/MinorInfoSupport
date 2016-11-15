using Minor.RoWe.Common.Interfaces;
using Minor.RoWe.Common.Events;
using Minor.RoWe.Eventbus.Options;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using RabbitMQ.Client.Framing;
using Minor.RoWe.Eventbus.Connectors;
using System;
using System.Diagnostics;

namespace Minor.RoWe.Eventbus.Publishers
{
    public class EventPublisher : IEventPublisher
    {
        private IRabbiMqConnection _connection;
        public EventPublisher(IRabbiMqConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="domainEvent"></param>
        public void Publish(DomainEvent domainEvent)
        {
            var json = JsonConvert.SerializeObject(domainEvent);
            var body = Encoding.UTF8.GetBytes(json);
            var basicProperties = new BasicProperties()
            {

                Type = domainEvent.GetType().AssemblyQualifiedName
            };
            _connection.Channel.BasicPublish(
                                 exchange: _connection.Options.ExchangeName,
                                 routingKey: domainEvent.RoutingKey,
                                 mandatory: false,
                                 basicProperties: basicProperties,
                                 body: body
            );
        }
    }
}
