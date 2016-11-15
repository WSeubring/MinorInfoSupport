using Minor.RoWe.Common.Events;
using Minor.RoWe.Common.Interfaces;
using Minor.RoWe.Eventbus.Connectors;
using Minor.RoWe.Eventbus.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Minor.RoWe.Eventbus.Dispatchers
{
    public abstract class EventDispatcher : IDisposable
    {
        private Dictionary<string, MethodInfo> _handlers;
        private IRabbiMqConnection _connection;
        public abstract string RoutingKey { get; }
        public EventDispatcher(IRabbiMqConnection connection)
        {
            _connection = connection;
            Setup();
            _handlers = new Dictionary<string, MethodInfo>();
            FindHandlers();
            //

        }

        private void FindHandlers()
        {
            var methods = GetType().GetMethods().Where(e => e.GetParameters().Count() == 1);

            methods = methods.Where(f => (typeof(DomainEvent).IsAssignableFrom(f.GetParameters().First().ParameterType)));

            foreach(var method in methods)
            {
                _handlers.Add(method.GetParameters().First().ParameterType.AssemblyQualifiedName, method);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _connection?.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Setup()
        {
            var _queue = _connection.Channel.QueueDeclare(queue: _connection.Options.QueueName, durable: true, exclusive: true, autoDelete: false, arguments: null);

            // foreach 
      
            _connection.Channel.QueueBind(queue: _queue,
                                            exchange: _connection.Options.ExchangeName,
                                            routingKey: RoutingKey);
         

            var consumer = new EventingBasicConsumer(_connection.Channel);
             consumer.Received += OnReceivedMessage;
            _connection.Channel.BasicConsume(queue: _queue,
                                 noAck: true,
                                 consumer: consumer);

       

        }

        private void OnReceivedMessage(object sender, BasicDeliverEventArgs e)
        {
            // decoding message 
            var json = Encoding.UTF8.GetString(e.Body);

            var type = e.BasicProperties.Type;

            var domainEvent = JsonConvert.DeserializeObject(json, Type.GetType(type));

            _handlers[type].Invoke(this, new object[] { domainEvent});
                          

    
        }
    }
}
