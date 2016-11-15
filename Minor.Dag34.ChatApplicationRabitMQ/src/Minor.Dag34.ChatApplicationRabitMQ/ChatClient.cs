using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minor.Dag34.ChatApplicationRabitMQ
{
    public class ChatClient
    {
        private static string _username;
        public static void Main(string[] args)
        {
            Console.WriteLine("Please enter your username: ");
            _username = Console.ReadLine();

            var factory = new ConnectionFactory() { HostName = "192.168.120.36", UserName="best", Password="best" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var reveiver = new MessageReceiver(channel);
                var sender = new MessageSender(channel);
                int counter = 1;
                while (true)
                {
                    var message = $"Bericht {counter++}";
                    sender.send(message);
                }
            }
        }
    }
}
