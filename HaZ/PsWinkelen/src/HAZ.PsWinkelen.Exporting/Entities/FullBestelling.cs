using System.Collections.Generic;

namespace HAZ.PsWinkelen.Exporting.Entities
{
    public class FullBestelling
    { 
        public IEnumerable<Artikel> Artikelen { get; set; }
        public Klant Klant { get; set; } 
    }
}
