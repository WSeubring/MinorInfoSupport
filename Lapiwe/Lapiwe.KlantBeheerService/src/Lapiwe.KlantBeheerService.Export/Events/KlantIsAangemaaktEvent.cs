using Lapiwe.Common.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lapiwe.KlantBeheerService.Export
{

    public class KlantIsAangemaaktEvent : DomainEvent
    {
        public Guid KlantGuid { get; set; }

        [Required]
        [StringLength(50)]
        public string Voornaam { get; set; }

        [StringLength(20)]
        public string Tussenvoegsel { get; set; }

        [Required]
        [StringLength(100)]
        public string Achternaam { get; set; }

        [StringLength(100)]
        public string Adres { get; set; }

        [StringLength(10)]
        public string Postcode { get; set; }

        [StringLength(50)]
        public string Woonplaats { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefoonnummer { get; set; }

        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        public KlantIsAangemaaktEvent(Guid klantGuid, string voornaam, string tussenvoegsel, string achternaam, string adres, string postcode, string woonplaats, string telefoonummer, string email)
        {
            KlantGuid = klantGuid;
            Voornaam = voornaam;
            Tussenvoegsel = tussenvoegsel;
            Achternaam = achternaam;
            Adres = adres;
            Postcode = postcode;
            Woonplaats = woonplaats;
            Telefoonnummer = telefoonummer;
            Email = email;
        }

        public KlantIsAangemaaktEvent()
        {

        }
    }

}