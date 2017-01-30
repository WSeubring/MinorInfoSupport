using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Lapiwe.EventBus.Domain;

namespace Lapiwe.EventBus.Common
{
    public class Channel : IDisposable
    {
        private IModel _model;
        private IConnection _connection;
        private BusOptions _busOptions;
        private string _queueName;

        public Channel(BusOptions busOptions)
        {
            _busOptions = busOptions;
            ConnectionFactory factory = new ConnectionFactory {
                HostName = busOptions.HostName,
                UserName = busOptions.Username,
                Password = busOptions.Password,
                Port = busOptions.Port,
                RequestedHeartbeat = 60
            };
            _connection = factory.CreateConnection();
            _model = _connection.CreateModel();
        }

        public void Publish(string routingKey, byte[] body, string eventType)
        {
            var prop = _model.CreateBasicProperties();
            prop.Type = eventType;
            _model.BasicPublish(_busOptions.ExchangeName, routingKey, false, prop, body);
        }

        internal void CreateBinding(string queueName, string routingKey)
        {
            DeclareExchange();
            if (!string.IsNullOrEmpty(queueName))
            {
                CreateQueue(queueName);
            }
            else
            {
                CreateExchangeQueueBinding(routingKey);
            }
        }

        internal void DeclareExchange()
        {
            _model.ExchangeDeclare(_busOptions.ExchangeName, ExchangeType.Topic);
        }

        internal void CreateExchangeQueueBinding(string routingKey)
        {
            _queueName = _model.QueueDeclare().QueueName;
            _model.QueueBind(queue: _queueName, exchange: _busOptions.ExchangeName, routingKey: routingKey);
        }

        internal void CreateQueue(string queueName)
        {
            _queueName = _model.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true, arguments: null);
        }

        public void Consume(EventHandler<BasicDeliverEventArgs> onReceivedEvent)
        {
            var consumer = new EventingBasicConsumer(_model);
            consumer.Received += onReceivedEvent;
            _model.BasicConsume(queue: _queueName, noAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _model?.Dispose();
            _connection?.Dispose();
        }
    }
}

