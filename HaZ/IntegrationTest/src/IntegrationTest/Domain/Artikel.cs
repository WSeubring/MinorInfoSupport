using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTest.Domain
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
        public decimal PrijsInclBtw { get; private set; }
        public decimal Prijs
        {
            get { return _prijs; }
            set
            {
                _prijs = value;
                PrijsInclBtw = Math.Round((_prijs + _prijs * (BTW_PERCENTAGE / 100)), 2);
            }
        }
        private decimal _prijs;
        public int Voorraad { get; set; }

        public string Leverancier { get; set; }
        // public IEnumerable<string> Categorieen { get; set; } = new List<string>();
        public bool InCatalog { get; set; }

    }
}
