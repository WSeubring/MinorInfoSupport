using HaZ.DSBestellingenBeheer.Entities;
using HaZ.DSBestellingenBeheer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaZ.DSBestellingenBeheer.Incoming.Commands;
using InfoSupport.WSA.Infrastructure;
using HaZ.DSBestellingenBeheer.Outgoing.Events;

namespace HaZ.DSBestellingenBeheer.Services
{
    public class BestellingService : IDisposable
    {
        private IRepository<Bestelling, int> _bestellingRepository;
        private IEventPublisher _eventPublisher;

        public BestellingService(IRepository<Bestelling, int> bestellingRepository, IEventPublisher eventpublisher)
        {
            _bestellingRepository = bestellingRepository;
            _eventPublisher = eventpublisher;
        }

        public virtual void Dispose()
        {
            _bestellingRepository?.Dispose();
            _eventPublisher?.Dispose();
        }

        public virtual void BestellingToevoegen(BestellingToevoegenCommand bestellingToevoegenCommand)
        {
            Bestelling bestelling = CreateBestellingFromBestellingToevoegenCommand(bestellingToevoegenCommand);

            _bestellingRepository.Insert(bestelling);

            BestellingToegevoegdEvent bestellingToegevoegdEvent = CreateBestellingToegevoegdEventFromBestelling(bestelling);
            
            _eventPublisher.Publish(bestellingToegevoegdEvent);
        }

        private BestellingToegevoegdEvent CreateBestellingToegevoegdEventFromBestelling(Bestelling bestelling)
        {
            BestellingToegevoegdEvent bestellingToegevoegdEvent = new BestellingToegevoegdEvent();
            bestellingToegevoegdEvent.Bestelnummer = bestelling.Id;
            bestellingToegevoegdEvent.DatumBestelling = bestelling.DatumBestelling;
            bestellingToegevoegdEvent.TotaalBedragInc = bestelling.TotaalBedragInc;
            bestellingToegevoegdEvent.TotaalBedragExc = bestelling.TotaalBedragExc;
            bestellingToegevoegdEvent.BestelStatus = bestelling.BestelStatus;
            bestellingToegevoegdEvent.Klantgegevens = new Outgoing.Events.Klantgegevens()
            {
                KlantId = bestelling.Klantgegevens.KlantId,
                Naam = bestelling.Klantgegevens?.Naam,
                Land = bestelling.Klantgegevens?.Land,
                Postcode = bestelling.Klantgegevens?.Postcode,
                Straatnaam = bestelling.Klantgegevens?.Straatnaam,
                Woonplaats = bestelling.Klantgegevens?.Woonplaats,
                Huisnummer = bestelling.Klantgegevens?.Huisnummer,
            };
            List<Outgoing.Events.Bestelregel> bestelregels = new List<Outgoing.Events.Bestelregel>();
            foreach (var item in bestelling?.Bestelregels)
            {
                Outgoing.Events.Bestelregel bestelregel = new Outgoing.Events.Bestelregel();
                bestelregel.ArtikelId = item.ArtikelId;
                bestelregel.AantalArtikelen = item.AantalArtikelen;
                bestelregel.ArtikelNaam = item.ArtikelNaam;
                bestelregel.ArtikelBeschrijving = item.ArtikelBeschrijving;
                bestelregel.PrijsPerArtikelInc = item.PrijsPerArtikelInc;
                bestelregel.PrijsPerArtikelExc = item.PrijsPerArtikelExc;
                bestelregel.LeverancierCode = item.LeverancierCode;
                bestelregel.AfbeeldingUrl = item.AfbeeldingUrl;

                bestelregels.Add(bestelregel);
            }
            bestellingToegevoegdEvent.Bestelregels = bestelregels;
            return bestellingToegevoegdEvent;
        }

        private Bestelling CreateBestellingFromBestellingToevoegenCommand(BestellingToevoegenCommand bestellingToevoegenCommand)
        {
            Bestelling bestelling = new Bestelling();
            bestelling.DatumBestelling = bestellingToevoegenCommand.DatumBestelling;
            bestelling.TotaalBedragInc = bestellingToevoegenCommand.TotaalBedragInc;
            bestelling.TotaalBedragExc = bestellingToevoegenCommand.TotaalBedragExc;
            bestelling.BestelStatus = bestellingToevoegenCommand.BestelStatus;
            bestelling.Klantgegevens = new Entities.Klantgegevens()
            {
                KlantId = bestellingToevoegenCommand.Klantgegevens.KlantId,
                Naam = bestellingToevoegenCommand.Klantgegevens?.Naam,
                Land = bestellingToevoegenCommand.Klantgegevens?.Land,
                Postcode = bestellingToevoegenCommand.Klantgegevens?.Postcode,
                Straatnaam = bestellingToevoegenCommand.Klantgegevens?.Straatnaam,
                Woonplaats = bestellingToevoegenCommand.Klantgegevens?.Woonplaats,
                Huisnummer = bestellingToevoegenCommand.Klantgegevens?.Huisnummer,
            };
            List<Entities.Bestelregel> bestelregels = new List<Entities.Bestelregel>();
            foreach (var item in bestellingToevoegenCommand?.Bestelregels)
            {
                Entities.Bestelregel bestelregel = new Entities.Bestelregel();
                bestelregel.ArtikelId = item.ArtikelId;
                bestelregel.AantalArtikelen = item.AantalArtikelen;
                bestelregel.ArtikelNaam = item.ArtikelNaam;
                bestelregel.ArtikelBeschrijving = item.ArtikelBeschrijving;
                bestelregel.PrijsPerArtikelInc = item.PrijsPerArtikelInc;
                bestelregel.PrijsPerArtikelExc = item.PrijsPerArtikelExc;
                bestelregel.AfbeeldingUrl = item.AfbeeldingUrl;
                bestelregel.LeverancierCode = item.LeverancierCode;

                bestelregels.Add(bestelregel);
            }
            bestelling.Bestelregels = bestelregels;
            return bestelling;
        }
    }
}
