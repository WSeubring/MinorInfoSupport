using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Domain.Infrastructure.Agents;
using System.Collections.Generic;
using System.Linq;

namespace HAZ.FeWebshop.Domain.Services
{
    public class BestellingService : IBestellingService
    {
        private IArtikelService _artikelService;
        private IWinkelenAgent _winkelAgent;

        public BestellingService(IWinkelenAgent winkelAgent, IArtikelService artikelService)
        {
            _winkelAgent = winkelAgent;
            _artikelService = artikelService;
        }

        public BestellingResult PlaatsBestelling(Bestelling bestelling)
        {
            var missingArtikelen = new List<int>();
            var artikelen = new List<Artikel>();
            foreach (var artikelnummer in bestelling.Artikelen)
            {
                var artikel = _artikelService.GetArtikel(artikelnummer);
                if (artikel == null)
                {
                    missingArtikelen.Add(artikelnummer);
                }
                else
                {
                    artikelen.Add(artikel);
                }
            }
            if (missingArtikelen.Count > 0)
            {
                return new BestellingResult()
                {
                    Errors = missingArtikelen.Select(artikelnummer => $"Artikel {artikelnummer} is niet meer beschikbaar").ToList()
                };
            }
            else
            {
                var fullBestelling = new FullBestelling
                {
                    Artikelen = artikelen,
                    Klant = bestelling.Klant
                };

                return _winkelAgent.PlaatsBestelling(fullBestelling);
            }
        }
    }
}