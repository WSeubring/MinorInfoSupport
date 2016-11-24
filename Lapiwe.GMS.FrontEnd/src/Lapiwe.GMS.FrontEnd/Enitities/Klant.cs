using System.ComponentModel.DataAnnotations;

namespace Lapiwe.GMS.FrontEnd.Enitities
{
    public class Klant
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Voornaam { get; set; }

        [StringLength(20)]
        public string Tussenvoegsel { get; set; }

        [StringLength(100)]
        public string Achternaam { get; set; }

        [StringLength(100)]
        public string Straatnaam { get; set; }

        [StringLength(6)]
        public string Huisnummer { get; set; }

        [StringLength(10)]
        public string Postcode { get; set; }

        [StringLength(100)]
        public string Woonplaats { get; set; }

        [StringLength(15)]
        public string Telefoonnummer { get; set; }

        [StringLength(128)]
        public string Emailadres { get; set; }
    }
}