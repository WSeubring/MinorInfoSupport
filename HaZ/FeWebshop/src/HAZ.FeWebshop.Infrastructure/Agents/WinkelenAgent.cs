using HAZ.FeWebshop.Domain.Infrastructure.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen;

namespace HAZ.FeWebshop.Infrastructure.Agents
{
    public class WinkelenAgent : IWinkelenAgent
    {
        private PsWinkelenClient _winkelenClient;

        public WinkelenAgent(WinkelAgentConfig config)
        {
            _winkelenClient = new PsWinkelenClient(config.WinkelenServiceUri);
        }

        public BestellingResult PlaatsBestelling(FullBestelling bestelling)
        {
            List<HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.Artikel> newArtikelList = new List<HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.Artikel>();
            HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.Klant newKlant = new HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.Klant
            {
                Naam = bestelling.Klant.Naam,
                KlantId = bestelling.Klant.KlantId,
                Land = bestelling.Klant.Land,
                Plaats = bestelling.Klant.Plaats,
                Huisnummer = bestelling.Klant.Huisnummer,
                Straatnaam = bestelling.Klant.Straatnaam,
                Postcode = bestelling.Klant.Postcode
            };

            foreach (Artikel artikel in bestelling.Artikelen)
            {
                HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.Artikel newArtikel = new HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.Artikel
                {
                    Naam = artikel.Naam,
                    Artikelnummer = artikel.Artikelnummer,
                    AfbeeldingUrl = artikel.AfbeeldingUrl,
                    Beschrijving = artikel.Beschrijving,
                    InCatalog = artikel.InCatalog,
                    Leverancier = artikel.Leverancier,
                    LeverancierCode = artikel.LeverancierCode,
                    LeverbaarTot = artikel.LeverbaarTot,
                    LeverbaarVanaf = artikel.LeverbaarVanaf,
                    Prijs = (double)artikel.Prijs,
                    PrijsInclBtw = (double)artikel.PrijsInclBtw
                };
                newArtikelList.Add(newArtikel);
            }

            var winkelenBestelling = new FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.FullBestelling()
            {
                
                Artikelen = newArtikelList,
                Klant = newKlant
            };

            var startBestellingCommand = new FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.FullBestelling()
            {
                Artikelen = newArtikelList,
                Klant = newKlant
            };

            HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.BestellingResult result = (HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.BestellingResult) _winkelenClient.StartBestelling(startBestellingCommand);
            List<HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.Artikel> artikelen = (List<HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.Artikel>) result.Bestelling.Artikelen;
            HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.Klant klant = result.Bestelling.Klant;

            List<Artikel> mappedArtikelList = new List<Artikel>();
            Klant mappedKlant = new Klant
            {
                Naam = klant.Naam,
                Huisnummer = klant.Huisnummer,
                Straatnaam = klant.Straatnaam,
                KlantId = (int) klant.KlantId,
                Land = klant.Land,
                Plaats = klant.Plaats,
                Postcode = klant.Postcode
            };

            foreach (HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models.Artikel artikel in artikelen)
            {
                Artikel ar = new Artikel
                {
                    Naam = artikel.Naam,
                    Beschrijving = artikel.Beschrijving,
                    InCatalog = (bool)artikel.InCatalog,
                    AfbeeldingUrl = artikel.AfbeeldingUrl,
                    Artikelnummer = (int)artikel.Artikelnummer,
                    Leverancier = artikel.Leverancier,
                    LeverancierCode = artikel.LeverancierCode,
                    LeverbaarTot = artikel.LeverbaarTot,
                    LeverbaarVanaf = (DateTime)artikel.LeverbaarVanaf,
                    Prijs = (decimal)artikel.Prijs
                };
                mappedArtikelList.Add(ar);
            }


            return new BestellingResult
            {
                Bestelling = new FullBestelling
                {
                    Artikelen = mappedArtikelList,
                    Klant = mappedKlant
                }
            };
        }
    }
}
