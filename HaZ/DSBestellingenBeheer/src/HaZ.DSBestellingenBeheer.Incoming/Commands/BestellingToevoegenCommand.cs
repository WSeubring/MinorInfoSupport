using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaZ.DSBestellingenBeheer.Incoming.Commands
{
    public class BestellingToevoegenCommand
    {
        public DateTime DatumBestelling { get; set; }
        public decimal TotaalBedragInc { get; set; }
        public decimal TotaalBedragExc { get; set; }
        public List<Bestelregel> Bestelregels { get; set; }
        public Klantgegevens Klantgegevens { get; set; }
        public string BestelStatus { get; set; }

        public BestellingToevoegenCommand()
        {

        }
    }
}
