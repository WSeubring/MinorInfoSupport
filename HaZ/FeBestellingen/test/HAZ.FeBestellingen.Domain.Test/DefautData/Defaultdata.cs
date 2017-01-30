using HaZ.DSBestellingenBeheer.Outgoing.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Domain.Test.DefautData
{
    public static class Defaultdata
    {
        public static BestellingToegevoegdEvent BestellingToegevoegdEvent()
        {
            List<Bestelregel> bestelregels = new List<Bestelregel>();
            bestelregels.Add(Bestelregel());
            
            return new BestellingToegevoegdEvent()
            {
                Bestelnummer = 1,
                BestelStatus = "picking",
                DatumBestelling = new DateTime(2017, 1, 1),
                Bestelregels = bestelregels,
                TotaalBedragInc = 100,
                TotaalBedragExc = 121,
                Klantgegevens = Klantgegevens()
            };
        }
        public static Bestelregel Bestelregel()
        {
            return new Bestelregel()
            {
                AantalArtikelen = 2,
                AfbeeldingUrl = "defaulturl.jpg",
                ArtikelBeschrijving = "De Black Cruiser is in mat zwart. In Amerika fietsen de stoere jongens langs de Beach. Dit wil natuurlijk elke stoere jongen.",
                ArtikelId = 1,
                ArtikelNaam = "Yipeeh Black Cruiser - Kinderfiets - 12 Inch - Jongens - Zwart",
                BestelregelId = 1,
                LeverancierCode = "DC",
                PrijsPerArtikelExc = 50m,
                PrijsPerArtikelInc = 61.50m
            };
        }

        public static Klantgegevens Klantgegevens()
        {
            return new Klantgegevens()
            {
                Huisnummer = "11a",
                KlantgegevensId = 1,
                Land ="Nederland",
                KlantId = 1,
                Naam = "Henk",
                Postcode = "3241 AB",
                Straatnaam = "Marktstraat",
                Woonplaats = "Utrecht" 
            };
        }
    }
}
