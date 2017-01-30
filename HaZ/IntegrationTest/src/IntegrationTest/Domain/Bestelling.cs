using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTest.Domain
{
    public class Bestelling
    {

        [Key]
        public int Bestelnummer { get; set; }
        public DateTime DatumBestelling { get; set; }

        public decimal TotaalBedragInc { get; set; }

        public decimal TotaalBedragExc { get; set; }

        public string BestelStatus { get; set; }

        public List<Bestelregel> Bestelregels { get; set; }

        public Klantgegevens Klantgegevens { get; set; }

       
    }
}
