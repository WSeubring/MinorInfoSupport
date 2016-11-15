using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Text;

namespace Minor.Dag34.ChatApplicationRabitMQ
{
    public class MessageSender
    {
        private IModel _channel;
        public MessageSender(IModel channel)
        {
            _channel = channel;
        }

        public void send(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "messages",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }

        internal void send(string message, string from)
        {
            
        }
    }
}
