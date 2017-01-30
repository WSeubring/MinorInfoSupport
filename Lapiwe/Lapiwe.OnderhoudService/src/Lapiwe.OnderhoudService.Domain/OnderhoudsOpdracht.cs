using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lapiwe.OnderhoudService.Domain
{
    public class OnderhoudsOpdracht : DomainEntity
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public Guid KlantGuid { get; set; }
        [Required]
        public Guid AutoGuid { get; set; }
        [Required]
        public DateTime AanmeldDatum { get; set; }
        [Required]
        public int Kilometerstand { get; set; }
        [Required]
        public bool Apk { get; set; }
        [Required]
        public string OpdrachtOmschrijving { get; set; }
        [Required]
        public Status OpdrachtStatus { get; set; }

        public string Werkzaamheden { get; set; }



    }
}
