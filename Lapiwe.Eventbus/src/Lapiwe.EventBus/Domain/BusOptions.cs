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

        public BusOptions()
        {
            HostName = "localhost";
            Port = 5672;
            Username = "guest";
            Password = "guest";
            ExchangeName = "Lapiwe.DefaultBus";
        }
    }
}