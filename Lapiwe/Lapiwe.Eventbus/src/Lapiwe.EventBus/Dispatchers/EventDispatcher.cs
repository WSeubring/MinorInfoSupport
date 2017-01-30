using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Lapiwe.EventBus.Common;
using Lapiwe.EventBus.Domain;
using Lapiwe.EventBus.Attributes;
using Lapiwe.Common.Domain;

namespace Lapiwe.EventBus.Dispatchers
{
    public abstract class EventDispatcher : IDisposable
    {
        private Channel _channel;
        private IConnection _connection;
        private Dictionary<string, MethodObject> _methods;

        public EventDispatcher(BusOptions options = null, string queueName = null)
        {
            var type = this.GetType();
            var routingKey = type.GetTypeInfo().GetCustomAttributes<RoutingKeyAttribute>().FirstOrDefault()?.Topic ?? "#";
            var attributeOptions = type.GetTypeInfo().GetCustomAttributes<BusOptions>().FirstOrDefault();
            var busOptions = options ?? attributeOptions ?? new BusOptions();

            _channel = new Channel(busOptions);
            _channel.CreateBinding(queueName, routingKey);

            SetupMethodInfo();

            _channel.Consume(OnReceivedEvent);
        }

        private void SetupMethodInfo()
        {
            var type = this.GetType();
            _methods = new Dictionary<string, MethodObject>();
            foreach (var method in type.GetMethods())
            {
                var parameters = method.GetParameters();
                if (parameters.Count() > 1)
                {
                    continue;
                }

                foreach (var parameter in parameters)
                {
                    if ((typeof(DomainEvent)).IsAssignableFrom(parameter.ParameterType))
                    {

                        try
                        {
                            _methods.Add(parameter.ParameterType.FullName, new MethodObject { type = parameter.ParameterType, methodInfo = method });
                        }
                        catch (ArgumentException)
                        {
                            Dispose();
                            throw new DuplicateMethodWithSameEventParameterException(method.ToString());
                        }
                    }
                }
            }
        }

        protected virtual void OnReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var typeEvent = e.BasicProperties.Type;
            var KeyValuePair = _methods.First(k => k.Key == typeEvent);

            var methodObject = KeyValuePair.Value;

            var message = Encoding.UTF8.GetString(e.Body);
            var domainEvent = JsonConvert.DeserializeObject(message, methodObject.type);
            methodObject.methodInfo.Invoke(this, new object[] { domainEvent });
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
