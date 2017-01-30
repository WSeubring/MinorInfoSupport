using System;

namespace HaZ.DSBestellingenBeheer.Incoming.Commands
{
    public class Bestelregel
    {

        public int ArtikelId { get; set; }        
        public int AantalArtikelen { get; set; }
        public string ArtikelBeschrijving { get; set; }
        public string ArtikelNaam { get; set; }
        public decimal PrijsPerArtikelInc { get; set; }
        public decimal PrijsPerArtikelExc { get; set; }
        public string AfbeeldingUrl { get; set; }
        public string LeverancierCode { get; set; }

        public Bestelregel()
        {

        }
    }
}