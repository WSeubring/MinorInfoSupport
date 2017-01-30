using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lapiwe.OnderhoudService.Export
{
    public class MeldOnderhoudsOpdrachtAanCommand : DomainCommand
    {
        [Required]
        public Guid KlantGuid { get; set; }
        [Required]
        public Guid AutoGuid { get; set; }
        [Required]
        public DateTime AanmeldDatum { get; set; }
        [Required]
        public int Kilometerstand { get; set; }
        [Required]
        public string OpdrachtOmschrijving { get; set; }
        [Required]
        public bool Apk { get; set; }

        public MeldOnderhoudsOpdrachtAanCommand()
        {

        }

        public MeldOnderhoudsOpdrachtAanCommand(Guid klantGuid, Guid autoGuid, DateTime aanmeldDatum, int kilometerstand, string opdrachtOmschrijving, bool apk) 
        {
            KlantGuid = klantGuid;
            AutoGuid = autoGuid;
            AanmeldDatum = aanmeldDatum;
            Kilometerstand = kilometerstand;
            OpdrachtOmschrijving = opdrachtOmschrijving;
            Apk = apk;
        }
    }
}
