namespace HAZ.FeBestellingen.Api.Models.FactuurViewModels
{
    public class BestellingRegelViewModel
    {
        public int AantalArtikelen { get; set; }
        public long ArtikelId { get; set; }
        public string ArtikelNaam { get; set; }
        public decimal PrijsPerArtikelInc { get; set; }
    }
}