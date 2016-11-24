using System;

namespace Lapiwe.Common
{ 
    public abstract class DomainCommand
    {
        public DateTime TimeStamp { get; set; }
        public Guid CorrelationID { get; set; }
        public string RoutingKey { get; set; }

        public DomainCommand()
        {
            TimeStamp = DateTime.Now;
            CorrelationID = new Guid();
            RoutingKey = GetType().FullName;
        }
    }
}
