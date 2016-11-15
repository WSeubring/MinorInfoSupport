using Minor.RoWe.Eventbus.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.RoWe.Eventbus.Connectors
{
    public class RabbitMqConnection : IRabbiMqConnection
    {
        public BusOptions Options { get; private set; }
        public IModel Channel { get; private set; }
        private IConnection _connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public RabbitMqConnection(BusOptions options)
        {
            Options = options;
            StartConnection();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Channel?.Dispose();
            _connection?.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        private void StartConnection()
        {
            var factory = new ConnectionFactory() { HostName = Options.HostName };
            _connection = factory.CreateConnection();
            Channel = _connection.CreateModel();
            Channel.ExchangeDeclare(exchange: Options.ExchangeName, type: ExchangeType.Topic, durable: true, autoDelete: false, arguments: null);
  
        }

    }
}
