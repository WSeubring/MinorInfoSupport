namespace HaZ.DSBestellingenBeheer.Outgoing.Events
{
    public class Bestelregel
    {
        public int BestelregelId { get; set; }
        public int AantalArtikelen { get; set; }
        public string ArtikelBeschrijving { get; set; }
        public long ArtikelId { get; set; }
        public string ArtikelNaam { get; set; }
        public decimal PrijsPerArtikelInc { get; set; }
        public decimal PrijsPerArtikelExc { get; set; }
        public string AfbeeldingUrl { get; set; }
        public string LeverancierCode { get; set; }
    }
}