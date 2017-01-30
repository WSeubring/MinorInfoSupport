using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Domain.Entities
{
    public class Bestelling
    {

        [Required(ErrorMessage = "Geen artikelen geselecteerd")]
        [MinLength(1, ErrorMessage = "Geen artikelen geselecteerd")]
        public List<int> Artikelen { get; set; }

        [Required(ErrorMessage = "Klant gegevens ontbreken")]
        public Klant Klant { get; set; }
    }
}
