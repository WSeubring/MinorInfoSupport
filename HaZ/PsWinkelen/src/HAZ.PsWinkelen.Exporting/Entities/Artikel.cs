using Kantilever.Catalogusbeheer.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HAZ.PsWinkelen.Exporting.Entities
{
    public class Artikel
    {
        [Key]
        public int Artikelnummer { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal Prijs { get; set; }

        private readonly decimal BTW_PERCENTAGE = 21;
        public decimal PrijsInclBtw { get; set; }
        public string AfbeeldingUrl { get; set; }
        public DateTime LeverbaarVanaf { get; set; }
        public DateTime? LeverbaarTot { get; set; }
        public string LeverancierCode { get; set; }

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
            PrijsInclBtw = artikelToegevoegdEvent.Prijs * (1 + (BTW_PERCENTAGE / 100));
            //Categorieen = artikelToegevoegdEvent.Categorieen.ToList();
            InCatalog = true;
        }
    }
}
