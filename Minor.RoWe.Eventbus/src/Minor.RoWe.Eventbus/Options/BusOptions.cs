using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.RoWe.Eventbus.Options
{
    public class BusOptions
    {
        public string ExchangeName { get; set; } = "TestExchange";
        public string QueueName { get; set; } = "TestQueue";
        public string HostName { get; set; } = "localhost";
        public int Port { get; set; } = 80;

        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest"; 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName"></param>
        public BusOptions()
        {
        }

    }
}
