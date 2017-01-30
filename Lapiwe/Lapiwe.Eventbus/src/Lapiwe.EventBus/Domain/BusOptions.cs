using System;

namespace Lapiwe.EventBus.Domain
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BusOptions : Attribute
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ExchangeName { get; set; }

        public BusOptions(string hostname = "rabbitmq", int port = 5672,
                          string username = "Lapiwe", string password = "Lapiwe123",
                          string exchangeName = "Lapiwe.GMS")
        {
            HostName = hostname;
            Port = port;
            Username = username;
            Password = password;
            ExchangeName = exchangeName;
        }
    }
}