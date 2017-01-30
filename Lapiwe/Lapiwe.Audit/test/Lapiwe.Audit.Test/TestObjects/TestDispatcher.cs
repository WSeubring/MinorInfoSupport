using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using System.Text;
using RabbitMQ.Client;
using Lapiwe.EventBus.Domain;

namespace Lapiwe.Audit.Test.TestObjects
{
    public class TestDispatcher : IDisposable
    {
        private IModel _channel;
        private IConnection _connection;

        public TestDispatcher(BusOptions options = null, string queuename = null)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queuename,
                     durable: false,
                     exclusive: false,
                     autoDelete: true,
                     arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                ReceivedTestEventCount++;
            };
            _channel.BasicConsume(queue: queuename,
                                 noAck: true,
                                 consumer: consumer);

        }

        public int ReceivedTestEventCount { get; internal set; }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
