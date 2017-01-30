using HAZ.PsWinkelen.Domain.Infrastructure.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HAZ.PsWinkelen.Domain.Models;

namespace HAZ.PsWinkelen.Infrastructure.Agents
{
    public class BestellingenBeheerServiceAgent : IBestellingenBeheerServiceAgent
    {
        private BestellingenBeheerService _bestellingenBeheerService;

        public BestellingenBeheerServiceAgent(BestellingenBeheerAgentConfig config)
        {
            _bestellingenBeheerService = new BestellingenBeheerService(config.BestellingenBeheerServiceUri);
        }

        public object PostBestellingToevoegen(BestellingToevoegenCommand command)
        {
            List<Bestelregel> regels = new List<Bestelregel>();
            foreach (Bestelregel regel in command.Bestelregels)
            {
                Bestelregel newRegel = new Bestelregel
                {
                    ArtikelId = regel.ArtikelId,
                    AantalArtikelen = regel.AantalArtikelen,
                    AfbeeldingUrl = regel.AfbeeldingUrl,
                    ArtikelBeschrijving = regel.ArtikelBeschrijving,
                    ArtikelNaam = regel.ArtikelNaam,
                    PrijsPerArtikelExc = regel.PrijsPerArtikelExc,
                    PrijsPerArtikelInc = regel.PrijsPerArtikelInc,
                    LeverancierCode = regel.LeverancierCode
                };
                regels.Add(newRegel);
            }
            BestellingToevoegenCommand newCommand = new BestellingToevoegenCommand
            {
                Klantgegevens = new Klantgegevens
                {
                    Naam = command.Klantgegevens.Naam,
                    Huisnummer = command.Klantgegevens.Huisnummer,
                    KlantId = command.Klantgegevens.KlantId,
                    Land = command.Klantgegevens.Land,
                    Postcode = command.Klantgegevens.Postcode,
                    Straatnaam = command.Klantgegevens.Straatnaam,
                    Woonplaats = command.Klantgegevens.Woonplaats
                },
                BestelStatus = command.BestelStatus,
                DatumBestelling = command.DatumBestelling,
                TotaalBedragInc = command.TotaalBedragInc,
                TotaalBedragExc = command.TotaalBedragExc,
                Bestelregels = regels
            };
            return _bestellingenBeheerService.PostBestellingToevoegen(newCommand);
        }
    }
}
