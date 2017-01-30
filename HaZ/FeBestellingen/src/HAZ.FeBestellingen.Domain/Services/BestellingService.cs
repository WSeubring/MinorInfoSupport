using HAZ.FeBestellingen.Domain.Entities;
using HAZ.FeBestellingen.Domain.Infrastructure.Repositories;
using HAZ.FeBestellingen.Outgoing;
using InfoSupport.WSA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Domain.Services
{
    public class BestellingService : IBestellingService
    {
        private IBestellingRepository _bestellingRepository;
        private IEventPublisher _eventpublisher;

        public BestellingService(IBestellingRepository bestellingRepository, IEventPublisher eventpublisher)
        {
            _bestellingRepository = bestellingRepository;
            _eventpublisher = eventpublisher;
        }

        public void AddBestelling(Bestelling bestelling)
        {
            if (!_bestellingRepository.Exist(bestelling.Bestelnummer))
            {
                _bestellingRepository.Insert(bestelling);
            }
            else
            {
                _bestellingRepository.Update(bestelling);
            }
        }

        public Bestelling GetNextBestelling()
        {
            Bestelling nextBestelling = null;
            nextBestelling = _bestellingRepository.GetBestellingWithBestellingregels();
            
            return nextBestelling; 
        }

        public void PickBestelling(int bestelnummer)
        {
            Bestelling bestelling = GetBestelling(bestelnummer);
            if(bestelling != null)
            {
                bestelling.BestelStatus = "picked";
                var pickedEvent = CreatePickedEvent(bestelling);
                _bestellingRepository.Update(bestelling);                                
                PublishBestellingGepickedEvent(pickedEvent);
            } else
            {
                throw new KeyNotFoundException("Bestelnummer not found");
            }
        }

        public void PublishBestellingGepickedEvent(BestellingGepickedEvent pickedEvent)
        {
            _eventpublisher.Publish(pickedEvent);            
        }

        private BestellingGepickedEvent CreatePickedEvent(Bestelling bestelling)
        {
            List<Artikel> artikelen = new List<Artikel>();
            foreach (var item in bestelling?.Bestelregels)
            {
                Artikel artikel = new Artikel();
                artikel.ArtikelID = unchecked((int)item.ArtikelId);
                artikel.AantalGepicked = item.AantalArtikelen;
                artikelen.Add(artikel);
            }
            return new BestellingGepickedEvent() { Artikelen = artikelen };
        }

        public Bestelling GetBestelling(int bestelnummer)
        {
            Bestelling bestelling = null;
            bestelling = _bestellingRepository.GetBestellingByID(bestelnummer);
            return bestelling;
        }

        public void Dispose()
        {
            _bestellingRepository?.Dispose();
            _eventpublisher?.Dispose();
        }
    }
}
