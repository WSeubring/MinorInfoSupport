using System;

namespace Lapiwe.Common
{ 
    public abstract class DomainEvent
    {
        public DateTime TimeStamp { get; set; }
        public Guid CorrelationID { get; set; }
        public string RoutingKey { get; set; }

        public DomainEvent()
        {
            TimeStamp = DateTime.Now;
            CorrelationID = new Guid();
            RoutingKey = GetType().FullName;
        }
    }
}
