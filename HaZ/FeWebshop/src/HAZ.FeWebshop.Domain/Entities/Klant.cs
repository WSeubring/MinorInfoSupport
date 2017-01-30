using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Domain.Entities
{
    public class Klant
    {
        //[Required]
        public int KlantId { get; set; }

        [Required(ErrorMessage = "Klant naam ontbreekt")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Land ontbreekt")]
        public string Land { get; set; }

        [Required(ErrorMessage = "Postcode ontbreekt")]
        [RegularExpression(@"(?:^[1-9]{1}[0-9]{3}$)|(?:^[1-9][0-9]{3}\s?([a-zA-Z]{2})?$)", ErrorMessage = "Ongeldige Postcode")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Huisnummer ontbreekt")]
        public string Huisnummer { get; set; }

        [Required(ErrorMessage = "Straatnaam ontbreekt")]
        public string Straatnaam { get; set; }

        [Required(ErrorMessage = "Plaats ontbreekt")]
        public string Plaats { get; set; }

    }
}
