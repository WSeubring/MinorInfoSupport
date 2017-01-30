using HAZ.FeBestellingen.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Api.Models.BestellingViewModels
{
    public class BestellingRegelViewModel
    {
        public BestellingRegelViewModel(Bestelregel bestelregel)
        {
            AfbeeldingUrl = bestelregel.AfbeeldingUrl;
            Naam = bestelregel.ArtikelNaam;
            LeveranciersCode = bestelregel.LeverancierCode;
            Aantal = bestelregel.AantalArtikelen;
        }

        public BestellingRegelViewModel() { }

        public string AfbeeldingUrl { get; set; }
        public string Naam { get; set; }
        public string LeveranciersCode { get; set; }
        public int Aantal { get; set; }
    }
}
