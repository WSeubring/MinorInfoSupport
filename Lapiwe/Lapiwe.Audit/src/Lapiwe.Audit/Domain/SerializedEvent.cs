using System;
using System.ComponentModel.DataAnnotations;

namespace Lapiwe.Audit.Domain
{
    public class SerializedEvent
    {
        public int ID { get; set; }
        public string RoutingKey { get; set; }
        public string EventType { get; set; }
        public string Body { get; set; }
        public DateTime TimeReceived { get; set; }

        public SerializedEvent()
        {
            TimeReceived = DateTime.Now;
        }
    }
}
