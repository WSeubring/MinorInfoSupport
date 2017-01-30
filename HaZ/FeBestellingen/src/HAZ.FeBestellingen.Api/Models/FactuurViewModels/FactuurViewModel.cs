using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Api.Models.FactuurViewModels
{
    public class FactuurViewModel
    {
        public int Bestelnummer { get; set; }
        public List<BestellingRegelViewModel> BestellingRegelViewModels { get; set; }
        public KlantViewModel KlantViewModel { get; set; }
        public decimal TotaalBedragInc { get; set; }
        public decimal TotaalBedragExc { get; set; }
    }
}
