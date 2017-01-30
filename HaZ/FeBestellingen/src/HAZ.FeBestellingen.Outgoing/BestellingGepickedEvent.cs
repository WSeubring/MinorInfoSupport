using InfoSupport.WSA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Outgoing
{
    public class BestellingGepickedEvent : DomainEvent
    {
        public List<Artikel> Artikelen { get; set; }
        public BestellingGepickedEvent() : base("HaZ.FeBestellingen.BestellingGepicked")
        {
        }
    }
}
