using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.IS.RDW.Export.Events
{
    public class KeuringVerwerktMetSteekproefEvent : DomainEvent
    {
        public Guid OnderhoudsGuid{ get; set; }
    }
}
