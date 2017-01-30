using InfoSupport.WSA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaZ.DSBestellingenBeheer.Outgoing.Events
{
    public class BestellingToegevoegdEvent : DomainEvent
    {
        public int Bestelnummer { get; set; }
        public DateTime DatumBestelling { get; set; }
        public decimal TotaalBedragInc { get; set; }
        public Klantgegevens Klantgegevens { get; set; }
        public List<Bestelregel> Bestelregels { get; set; }
        public string BestelStatus { get; set; }
        public decimal TotaalBedragExc { get; set; }

        public BestellingToegevoegdEvent() : base("HaZ.DSBestellingenBeheer.BestellingToegevoegd")
        {
        }
    }
}
