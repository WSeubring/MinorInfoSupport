using Lapiwe.EventBus.Dispatchers;
using Lapiwe.EventBus.Domain;
using Lapiwe.GMS.FrontEnd.DAL;
using Lapiwe.GMS.FrontEnd.Entities;
using Lapiwe.IS.RDW.Export.Events;
using Lapiwe.OnderhoudService.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.Dispatchers
{
    public class OnderhoudDispatcher : EventDispatcher
    {
        private ISimpleRepository _repository;

        public OnderhoudDispatcher(ISimpleRepository repository, BusOptions options = null) : base(options)
        {
            _repository = repository;
        }

        public void KeuringsVerzoekVerwerktZonderSteekproef(KeuringVerwerktZonderSteekproefEvent domainEvent)
        {
            KeuringsVerzoek verzoek = new KeuringsVerzoek(domainEvent.OnderhoudsGuid, true);

            _repository.Add(verzoek);
        }

        public void KeuringsVerzoekVerwerktMetSteekproef(KeuringVerwerktMetSteekproefEvent domainEvent)
        {
            KeuringsVerzoek verzoek = new KeuringsVerzoek(domainEvent.OnderhoudsGuid, false);

            _repository.Add(verzoek);
        }

        public void OnderhoudsOpdrachtGeregistreerd(OnderhoudsOpdrachtAangemeldEvent domainEvent)
        {
            OnderhoudsOpdracht onderhoudsOpdracht = new OnderhoudsOpdracht(
                domainEvent.OnderhoudsOpdrachtGuid,
                _repository.LazyLoadFind<Klant>(domainEvent.KlantGuid),
                _repository.LazyLoadFind<Auto>(domainEvent.AutoGuid),
                domainEvent.OpdrachtOmschrijving
            );
            onderhoudsOpdracht.Apk = domainEvent.Apk;

            _repository.Add(onderhoudsOpdracht);
        }
    }
}
