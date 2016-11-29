using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.IS.RDW.Export.Commands
{
    public class KeuringsVerzoekCommand
    {
        public Guid OnderhoudsGuid { get; set; }
        public string Kenteken { get; set; }
        public int Kilometerstand { get; set; }
        public string Klantnaam { get; set; }
    }
}
