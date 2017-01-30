using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Exporting.Entities
{
    public class Klant
    {
        //[Required]
        public int KlantId { get; set; }
        public string Naam { get; set; }
        public string Land { get; set; }
        public string Postcode { get; set; }
        public string Huisnummer { get; set; }
        public string Straatnaam { get; set; } 
        public string Plaats { get; set; }

    }
}
