using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.Entities
{
    public class KeuringsVerzoek : DomainEntity
    {
        [Key]
        public long ID { get; set; }
        public string Telefoonnummer { get; set; }
        public Guid OnderhoudsOpdrachtGuid { get; set; }
        public bool HeeftSteekproef { get; set; }

        // Needed for Entity Framework
        // Do not use yourself
        [Obsolete]
        public KeuringsVerzoek()
        {
        }

        public KeuringsVerzoek(Guid onderhoudsOpdrachtGuid, bool steekProef)
        {
            // TODO: Real number from UC01
            Telefoonnummer = "(+31) 6 70 377 264";
            OnderhoudsOpdrachtGuid = onderhoudsOpdrachtGuid;
            HeeftSteekproef = steekProef;
        }
    }
}
