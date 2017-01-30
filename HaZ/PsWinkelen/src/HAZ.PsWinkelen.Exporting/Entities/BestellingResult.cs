using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace HAZ.PsWinkelen.Exporting.Entities
{
    public class BestellingResult : ObjectResult
    {
        public BestellingResult(FullBestelling value) : base(value)
        {
            Bestelling = value;
        }

        public FullBestelling Bestelling { get; set; }

        public bool IsValid
        {
            get
            {
                return Bestelling != null;
            }
        }

        public List<string> Errors { get; set; }
    }
}
