using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Domain.Entities
{
    public class BestellingResult
    {

        public FullBestelling Bestelling { get; set; }

        public bool IsValid {
            get {
                return Bestelling != null;
            }
        }

        public List<string> Errors { get; set; }

    }
}
