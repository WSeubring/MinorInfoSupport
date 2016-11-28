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

        public BusOptions(string hostname = "localhost", int port = 5672,
                          string username = "guest", string password = "guest",
                          string exchangeName = "guest")
        {
            HostName = hostname;
            Port = port;
            Username = username;
            Password = password;
            ExchangeName = exchangeName;
        }
    }
}