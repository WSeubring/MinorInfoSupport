namespace HaZ.DSBestellingenBeheer.Outgoing.Events
{
    public class Klantgegevens
    {
        public int KlantgegevensId { get; set; }
        public string Huisnummer { get; set; }
        public long KlantId { get; set; }
        public string Land { get; set; }
        public string Naam { get; set; }
        public string Postcode { get; set; }
        public string Straatnaam { get; set; }
        public string Woonplaats { get; set; }
    }
}