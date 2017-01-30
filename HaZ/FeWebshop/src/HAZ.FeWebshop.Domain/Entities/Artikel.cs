using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Kantilever.Catalogusbeheer.Events;
using Kantilever.Magazijnbeheer;
using Kantilever.Magazijnbeheer.Events;

namespace HAZ.FeWebshop.Domain.Entities
{
    public class Artikel
    {
        private readonly decimal BTW_PERCENTAGE = 21;

        [Key]
        public int Artikelnummer { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public string AfbeeldingUrl { get; set; }
        public DateTime LeverbaarVanaf { get; set; }
        public DateTime? LeverbaarTot { get; set; }
        public string LeverancierCode { get; set; }
        public  decimal PrijsInclBtw{ get; private set; }
        public decimal Prijs
        {
            get { return _prijs; }
            set {
                _prijs = value;
                PrijsInclBtw = Math.Round((_prijs + _prijs * (BTW_PERCENTAGE / 100)), 2);
            }
        }
        private decimal _prijs;
        public int Voorraad { get; set; }

        public string Leverancier { get; set; }
       // public IEnumerable<string> Categorieen { get; set; } = new List<string>();
        public bool InCatalog { get; set; }

        public void ReplayCatalogusToegevoegdEvent(ArtikelAanCatalogusToegevoegd artikelToegevoegdEvent)
        {
            Artikelnummer = artikelToegevoegdEvent.Artikelnummer;
            Naam = artikelToegevoegdEvent.Naam;
            Beschrijving = artikelToegevoegdEvent.Beschrijving;
            Prijs = artikelToegevoegdEvent.Prijs;
            AfbeeldingUrl = artikelToegevoegdEvent.AfbeeldingUrl;
            LeverbaarVanaf = artikelToegevoegdEvent.LeverbaarVanaf;
            LeverbaarTot = artikelToegevoegdEvent.LeverbaarTot;
            LeverancierCode = artikelToegevoegdEvent.LeverancierCode;
            Leverancier = artikelToegevoegdEvent.Leverancier;
            //Categorieen = artikelToegevoegdEvent.Categorieen.ToList();
            InCatalog = true;
        }

        public void ReplayInMagazijnGezetEvent(ArtikelInMagazijnGezet artikelInMagazijnGezetEvent)
        {
            Voorraad = artikelInMagazijnGezetEvent.Voorraad;
        }

        public void ReplayUitMagazijnGehaaldEvent(ArtikelUitMagazijnGehaald artikelUitMagazijnGehaaldEvent)
        {
            Voorraad = artikelUitMagazijnGehaaldEvent.Voorraad;
        }
    }
}
