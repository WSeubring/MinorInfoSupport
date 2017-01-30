using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.Common.Domain
{
    public class DomainCommand
    {
        public DateTime TimeStamp { get; set; }
        public Guid CorrelationID { get; set; }

        public DomainCommand()
        {
            TimeStamp = DateTime.Now;
            CorrelationID = Guid.NewGuid();
        }
    }
}
