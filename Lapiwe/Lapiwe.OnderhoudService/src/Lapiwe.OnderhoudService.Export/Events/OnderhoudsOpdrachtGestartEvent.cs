using Lapiwe.Common.Domain;
using System;

namespace Lapiwe.OnderhoudService.Export
{
    public class OnderhoudsOpdrachtGestartEvent : DomainEvent
    {
        public Guid OnderhoudsOpdrachtGuid { get; set; }

        public OnderhoudsOpdrachtGestartEvent(Guid onderhoudsOpdrachtGuid)
        {
            OnderhoudsOpdrachtGuid = onderhoudsOpdrachtGuid;
        }

        public OnderhoudsOpdrachtGestartEvent() { }
    }

}