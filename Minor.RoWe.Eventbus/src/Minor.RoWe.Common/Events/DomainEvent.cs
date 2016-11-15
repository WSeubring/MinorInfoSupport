using System;

namespace Minor.RoWe.Common.Events
{
    public abstract class DomainEvent
    {

        public DateTime TimpeStamp { get; set; }
        public string RoutingKey { get; set; }
        public Guid CorrelationID { get; set; }
  
    }
}