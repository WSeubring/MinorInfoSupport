using Lapiwe.Common.Domain;
using System;

namespace Lapiwe.Audit.Domain
{
    public class SendAllEventCommand : DomainEvent
    {
        public DateTime? StartTime;
        public DateTime? EndTime;
        public string returnQueueName;
    }

}