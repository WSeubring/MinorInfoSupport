using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace HaZ.DSBestellingenBeheer.Entities
{
    public class Bestelling
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DatumBestelling { get; set; }

        [Required]
        public decimal TotaalBedragInc { get; set; }

        [Required]
        public decimal TotaalBedragExc { get; set; }

        public string BestelStatus { get; set; }

        public List<Bestelregel> Bestelregels { get; set; }

        [Required]
        public Klantgegevens Klantgegevens { get; set; }
    }
}
