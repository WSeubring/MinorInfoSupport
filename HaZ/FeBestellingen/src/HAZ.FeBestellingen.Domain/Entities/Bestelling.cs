using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaZ.DSBestellingenBeheer.Outgoing.Events;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace HAZ.FeBestellingen.Domain.Entities
{
    public class Bestelling
    {
        [Key]
        public int Bestelnummer { get; set; }
        public DateTime DatumBestelling { get; set; }
        
        public decimal TotaalBedragInc { get; set; }

        public decimal TotaalBedragExc { get; set; }

        public string BestelStatus { get; set; }

        public List<Bestelregel> Bestelregels { get; set; }
        
        public Klantgegevens Klantgegevens { get; set; }

        public void ReplayBestellingToegevoegdEvent(BestellingToegevoegdEvent bestellingToegevoegdEvent)
        {
            Bestelnummer = bestellingToegevoegdEvent.Bestelnummer;
            DatumBestelling = bestellingToegevoegdEvent.DatumBestelling;
            TotaalBedragInc = bestellingToegevoegdEvent.TotaalBedragInc;
            TotaalBedragExc = bestellingToegevoegdEvent.TotaalBedragExc;
            BestelStatus = bestellingToegevoegdEvent.BestelStatus;

            Bestelregels = new List<Bestelregel>();
            foreach (var item in bestellingToegevoegdEvent?.Bestelregels)
            {
                Bestelregel bestelregel = new Bestelregel();
                bestelregel.AantalArtikelen = (int)item?.AantalArtikelen;
                bestelregel.AfbeeldingUrl = item?.AfbeeldingUrl;
                bestelregel.ArtikelBeschrijving = item?.ArtikelBeschrijving;
                bestelregel.ArtikelId = (int)item?.ArtikelId;
                bestelregel.ArtikelNaam = item?.ArtikelNaam;
                bestelregel.BestelregelId = (int)item?.BestelregelId;
                bestelregel.LeverancierCode = item?.LeverancierCode;
                bestelregel.PrijsPerArtikelExc = (decimal)item?.PrijsPerArtikelExc;
                bestelregel.PrijsPerArtikelInc = (decimal)item?.PrijsPerArtikelInc;

                Bestelregels.Add(bestelregel);
            }
            Klantgegevens = new Klantgegevens() {
                KlantgegevensId = (int)bestellingToegevoegdEvent?.Klantgegevens?.KlantgegevensId,
                Huisnummer = bestellingToegevoegdEvent?.Klantgegevens?.Huisnummer,
                KlantId = (int)bestellingToegevoegdEvent?.Klantgegevens?.KlantId,
                Land = bestellingToegevoegdEvent?.Klantgegevens?.Land,
                Naam = bestellingToegevoegdEvent?.Klantgegevens?.Naam,
                Postcode = bestellingToegevoegdEvent?.Klantgegevens?.Postcode,
                Straatnaam = bestellingToegevoegdEvent?.Klantgegevens?.Straatnaam,
                Woonplaats = bestellingToegevoegdEvent?.Klantgegevens?.Woonplaats
            };
        }
    }
}
