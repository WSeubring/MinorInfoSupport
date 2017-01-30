using HAZ.PsWinkelen.Exporting.Entities;
using Kantilever.Catalogusbeheer.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Api.Test.DefaultData
{
    public class DefaultData
    {
        public static ArtikelAanCatalogusToegevoegd ArtikelAanCatalogusToegevoegdEvent()
        {
            return new ArtikelAanCatalogusToegevoegd()
            {
                AfbeeldingUrl = "http://Images.google.com/fietsbel",
                Artikelnummer = 324,
                Beschrijving = "Een fietbel voor de aller jongste",
                Leverancier = "Giant",
                LeverancierCode = "GIA",
                LeverbaarTot = DateTime.UtcNow.AddDays(1),
                LeverbaarVanaf = DateTime.UtcNow,
                Naam = "Kinder fietsbel", 
                Prijs = 10.0m
            };
        }

        public static FullBestelling StartBestellingCommand()
        {
            return new FullBestelling
            {
                Artikelen = new List<Artikel>() { new Artikel
                {
                    AfbeeldingUrl = "http://Images.google.com/fietsbel",
                    Artikelnummer = 324,
                    Beschrijving = "Een fietbel voor de aller jongste",
                    Leverancier = "Giant",
                    LeverancierCode = "GIA",
                    LeverbaarTot = DateTime.UtcNow.AddDays(1),
                    LeverbaarVanaf = DateTime.UtcNow,
                    Naam = "Kinder fietsbel",
                    Prijs = 10.0m
                } },
                Klant = new Klant
                {
                    Huisnummer = "10",
                    KlantId = 1,
                    Land = "Nederland",
                    Naam = "Ernst",
                    Plaats = "Ede",
                    Postcode = "9782VS",
                    Straatnaam = "Marktstraat"
                }
            };
        }
    }
}
