namespace HaZ.DSBestellingenBeheer.Incoming.Commands
{
    public class Klantgegevens
    {
        public int KlantId { get; set; }
        public string Naam { get; set; }
        public string Land { get; set; }
        public string Straatnaam { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }

        public Klantgegevens()
        {
        }        
    }
}