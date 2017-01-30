using System.ComponentModel.DataAnnotations;

namespace HaZ.DSBestellingenBeheer.Entities
{
    public class Klantgegevens
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int KlantId { get; set; }

        [Required]
        public string Naam { get; set; }

        [Required]
        public string Land { get; set; }

        [Required]
        public string Straatnaam { get; set; }

        [Required]
        public string Huisnummer { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string Woonplaats { get; set; }
    }
}