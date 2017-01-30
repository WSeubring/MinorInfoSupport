using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Api.Models.BestellingViewModels
{
    public class BestellingViewModel
    {
        public int Bestelnummer { get; set; }
        public List<BestellingRegelViewModel> BestellingRegelViewModels { get; set; }

        public BestellingViewModel()
        {
            BestellingRegelViewModels = new List<BestellingRegelViewModel>();
        }
    }
}
