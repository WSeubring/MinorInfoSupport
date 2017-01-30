using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Exporting.Entities
{
    public class Bestelling
    {
        public Guid BestellingId;
        public List<Guid> Artikelen { get; set; }
        public Klant Klant { get; set; }
    }
}
