using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Domain.Entities
{
    public class FullBestelling
    {
        
        public IEnumerable<Artikel> Artikelen { get; set; }
        public Klant Klant { get; set; }

    }
}
