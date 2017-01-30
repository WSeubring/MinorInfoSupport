using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.OnderhoudService.Export
{
    public class OnderhoudsOpdrachtAangemeldEvent : DomainEvent
    {
        public Guid OnderhoudsOpdrachtGuid { get; set; }
        public Guid KlantGuid { get; set; }
        public Guid AutoGuid { get; set; }
        public DateTime AanmeldDatum { get; set; }
        public int Kilometerstand { get; set; }
        public string OpdrachtOmschrijving { get; set; }
        public bool Apk { get; set; }

        public OnderhoudsOpdrachtAangemeldEvent(Guid opdrachtGuid, Guid klantGuid, Guid autoGuid, DateTime aanmeldDatum, int kilometerstand, string opdrachtOmschrijving, bool apk)
        {
            OnderhoudsOpdrachtGuid = opdrachtGuid;
            KlantGuid = klantGuid;
            AutoGuid = autoGuid;
            AanmeldDatum = aanmeldDatum;
            Kilometerstand = kilometerstand;
            OpdrachtOmschrijving = opdrachtOmschrijving;
            Apk = apk;
        }

        public OnderhoudsOpdrachtAangemeldEvent()
        {

        }
    }
}
