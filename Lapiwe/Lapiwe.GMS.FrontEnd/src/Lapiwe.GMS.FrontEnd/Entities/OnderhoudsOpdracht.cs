using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.Entities
{
    public class OnderhoudsOpdracht : DomainEntity
    {
        [Key]
        public long ID { get; set; }
        public Klant Klant { get; set; }
        public Auto Auto { get; set; }
        public string OpdrachtOmschrijving { get; set; }
        public bool Apk { get; set; }

        public OnderhoudsOpdracht()
        {

        }

        public OnderhoudsOpdracht(Guid onderhoudsOpdrachtGuid, Klant klant, Auto auto, string opdrachtOmschrijving) : base(onderhoudsOpdrachtGuid)
        {
            Klant = klant;
            Auto = auto;
            OpdrachtOmschrijving = opdrachtOmschrijving;
        }
    }
}
