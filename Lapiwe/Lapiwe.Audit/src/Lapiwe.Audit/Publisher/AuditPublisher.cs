using Lapiwe.Audit.Domain;
using Lapiwe.EventBus.Domain;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Lapiwe.Audit.Publisher
{
    public class AuditPublisher : IPublisher
    {
        private IModel _model;
        private IConnection _connection;

        public AuditPublisher(BusOptions options = null)
        {
            var busOptions = options ?? new BusOptions();
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = busOptions.HostName,
                UserName = busOptions.Username,
                Password = busOptions.Password,
                Port = busOptions.Port,
            };
            _connection = factory.CreateConnection();
            _model = _connection.CreateModel();
        }

        public void Publish(string queueName, SerializedEvent message)
        {
            _model.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: true,
                     arguments: null);

            var body = Encoding.UTF8.GetBytes(message.Body);
            var prop = _model.CreateBasicProperties();
            prop.Type = message.EventType;
            _model.BasicPublish(exchange: "", routingKey: queueName, mandatory: false, basicProperties: prop, body: body);
        }

        public void Dispose()
        {
            _model?.Dispose();
            _connection?.Dispose();
        }
    }
}