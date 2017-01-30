using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Net;
using Microsoft.Extensions.Logging;
using HAZ.PsWinkelen.Exporting.Entities;
using HAZ.PsWinkelen.Domain.Services;
using HAZ.PsWinkelen.Domain.Infrastructure.Agents;
using HAZ.PsWinkelen.Infrastructure.Agents;
using HAZ.PsWinkelen.Domain.Models;

namespace HAZ.PsWinkelen.API.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private IBestellingenBeheerServiceAgent _bestellingenBeheerServiceAgent;
        private IArtikelService _artikelService;
        private ILogger<HomeController> _logger;

        public HomeController(IBestellingenBeheerServiceAgent bestellingenBeheerServiceAgent, IArtikelService artikelService, ILogger<HomeController> logger)
        {
            _bestellingenBeheerServiceAgent = bestellingenBeheerServiceAgent;
            _artikelService = artikelService;
            _logger = logger;
        }

        [HttpPost]
        [SwaggerOperation("StartBestelling")]
        [ProducesResponseType(typeof(BestellingResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public ObjectResult StartBestelling([FromBody] FullBestelling StartBestellingCommand)
        {
            try
            {
                if (StartBestellingCommand != null)
                {
                    Klantgegevens klantGegevens = MapKlantgegevens(StartBestellingCommand);
                    List<Bestelregel> bestelRegels;
                    List<Artikel> foundArtikelen;

                    try
                    {
                        bestelRegels = new List<Bestelregel>();
                        foundArtikelen = new List<Artikel>();

                        foreach (Artikel artikel in StartBestellingCommand.Artikelen)
                        {
                            if (!bestelRegels.Exists(a => a.ArtikelId == artikel.Artikelnummer))
                            {
                                Artikel foundArtikel = _artikelService.GetArtikel(artikel.Artikelnummer);

                                if (foundArtikel != null)
                                {
                                    Bestelregel bestelRegel = mapBestelRegel(StartBestellingCommand, artikel, foundArtikel);
                                    bestelRegels.Add(bestelRegel);
                                    foundArtikelen.Add(foundArtikel);
                                }
                                else
                                {
                                    throw new ArtikelNotFoundException("Artikel met ID: " + artikel.Artikelnummer + " niet gevonden.");
                                }
                            }
                        }
                    }
                    catch (ArtikelNotFoundException e)
                    {
                        _logger.LogError(e.Message);
                        var error = new ErrorMessage(30, e.Message);
                        return BadRequest(error);
                    }

                    BestellingToevoegenCommand command = GenerateBestellingToevoegenCommand(klantGegevens, bestelRegels, foundArtikelen);

                    var response = _bestellingenBeheerServiceAgent.PostBestellingToevoegen(command);
                    BestellingResult result = MapBestellingResult(StartBestellingCommand, klantGegevens);
                    return Ok(result);
                }
                else
                {
                    var error = new ErrorMessage(30, $"An error occured: No command received");
                    return new ObjectResult(BadRequest());
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new ObjectResult(StatusCode(500));
            }
        }

        private static BestellingResult MapBestellingResult(FullBestelling StartBestellingCommand, Klantgegevens klantGegevens)
        {
            return new BestellingResult(
                new FullBestelling
                {
                    Klant = new Klant
                    {
                        Naam = klantGegevens.Naam,
                        Land = klantGegevens.Land,
                        KlantId = (int)klantGegevens.KlantId,
                        Huisnummer = klantGegevens.Huisnummer,
                        Plaats = klantGegevens.Woonplaats,
                        Postcode = klantGegevens.Postcode,
                        Straatnaam = klantGegevens.Straatnaam
                    },
                    Artikelen = StartBestellingCommand.Artikelen
                }
            );
        }

        private BestellingToevoegenCommand GenerateBestellingToevoegenCommand(Klantgegevens klantGegevens, List<Bestelregel> bestelRegels, List<Artikel> foundArtikelen)
        {
            return new BestellingToevoegenCommand
            {
                TotaalBedragExc = (double)foundArtikelen.Sum(a => a.Prijs),
                TotaalBedragInc = (double)foundArtikelen.Sum(a => a.PrijsInclBtw),
                Klantgegevens = klantGegevens,
                Bestelregels = bestelRegels,
                DatumBestelling = DateTime.Now,
                BestelStatus = "geplaatst"
            };
        }

        private Bestelregel mapBestelRegel(FullBestelling StartBestellingCommand, Artikel artikel, Artikel foundArtikel)
        {
            return new Bestelregel
            {
                ArtikelId = artikel.Artikelnummer,
                AantalArtikelen = StartBestellingCommand.Artikelen.Count(a => a.Artikelnummer == artikel.Artikelnummer),
                ArtikelBeschrijving = foundArtikel.Beschrijving,
                ArtikelNaam = foundArtikel.Naam,
                PrijsPerArtikelExc = (double)foundArtikel.Prijs,
                PrijsPerArtikelInc = (double)foundArtikel.PrijsInclBtw,
                AfbeeldingUrl = foundArtikel.AfbeeldingUrl,
                LeverancierCode = foundArtikel.LeverancierCode
            };
        }

        private Klantgegevens MapKlantgegevens(FullBestelling StartBestellingCommand)
        {
            return new Klantgegevens
            {
                Naam = StartBestellingCommand.Klant.Naam,
                Postcode = StartBestellingCommand.Klant.Postcode,
                KlantId = 0,
                Huisnummer = StartBestellingCommand.Klant.Huisnummer,
                Land = StartBestellingCommand.Klant.Land,
                Straatnaam = StartBestellingCommand.Klant.Straatnaam,
                Woonplaats = StartBestellingCommand.Klant.Plaats
            };
        }
    }
}
