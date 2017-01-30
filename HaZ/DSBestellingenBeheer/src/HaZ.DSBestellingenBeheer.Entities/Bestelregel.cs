using System.ComponentModel.DataAnnotations;

namespace HaZ.DSBestellingenBeheer.Entities
{
    public class Bestelregel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ArtikelId { get; set; }

        [Required]
        public string ArtikelNaam { get; set; }


        public string ArtikelBeschrijving { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int AantalArtikelen { get; set; }

        [Required]
        public decimal PrijsPerArtikelInc { get; set; }

        [Required]
        public decimal PrijsPerArtikelExc { get; set; }

        public string LeverancierCode { get; set; }

        public string AfbeeldingUrl { get; set; }
    }
}