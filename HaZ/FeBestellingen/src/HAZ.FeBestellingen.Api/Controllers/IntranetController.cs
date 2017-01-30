using HAZ.FeBestellingen.Api.Models.BestellingViewModels;
using HAZ.FeBestellingen.Api.Models.FactuurViewModels;
using HAZ.FeBestellingen.Domain.Entities;
using HAZ.FeBestellingen.Domain.Infrastructure.Agents;
using HAZ.FeBestellingen.Domain.Services;
using InfoSupport.WSA.Infrastructure;
using Kantilever.Magazijnbeheer.Commands;
using Kantilever.Magazijnbeheer.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace HAZ.FeBestellingen.Api.Controllers
{
    public class IntranetController : Controller
    {
        private ILogger<IntranetController> _logger;
        private IBestellingService _bestellingService;
        private IMagazijnBeheerAgent _magazijnBeheerAgent;

        public IntranetController(ILogger<IntranetController> logger, IBestellingService bestellingService, IMagazijnBeheerAgent magazijnBeheerAgent)
        {
            _logger = logger;
            _bestellingService = bestellingService;
            _magazijnBeheerAgent = magazijnBeheerAgent;
        }

        public IActionResult Index()
        {
            try
            {
                BestellingViewModel bestellingViewModel = new BestellingViewModel();
                Bestelling nextBestelling = null;
                using (var bestellingService = _bestellingService)
                {
                    nextBestelling = _bestellingService.GetNextBestelling();
                }
                if (nextBestelling != null)
                {
                    _logger.LogInformation($"Next bestelling is opgehaald, bestelnummer: {nextBestelling.Bestelnummer}");
                    bestellingViewModel.Bestelnummer = nextBestelling.Bestelnummer;
                    foreach (Bestelregel bestelregel in nextBestelling.Bestelregels)
                    {
                        Models.BestellingViewModels.BestellingRegelViewModel bestellingRegelViewModel = new Models.BestellingViewModels.BestellingRegelViewModel(bestelregel);
                        bestellingViewModel.BestellingRegelViewModels.Add(bestellingRegelViewModel);
                    }
                    return View("Index", bestellingViewModel);
                }
                return Pause();
            }
            catch (Exception e)
            {
                _logger.LogCritical($"An unexpected error occured in IntranetController.Index \n Errormessage: {e.Message} \n Stacktrace: {e.StackTrace}");
                return View("Oeps");
            }
        }

        public IActionResult Pause()
        {
            return View("Pause");
        }

        public IActionResult Factuur(int bestelnummer)
        {
            try
            {
                Bestelling bestelling = null;
                using (var bestellingService = _bestellingService)
                {
                    bestelling = _bestellingService.GetBestelling(bestelnummer);
                }
                if (bestelling != null)
                {
                    _logger.LogInformation($"Factuur bestelling opgehaald, bestelnummer: {bestelling.Bestelnummer}");
                    FactuurViewModel viewModel = MapBestellingToViewModel(bestelling);
                    return View("Factuur", viewModel);
                }
                else
                {
                    return Pause();
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical($"An unexpected error occured in IntranetController.Factuur, bestelnummer: {bestelnummer} \n Errormessage: {e.Message} \n Stacktrace: {e.StackTrace}");
                return View("Oeps");
            }
        }

        private FactuurViewModel MapBestellingToViewModel(Bestelling bestelling)
        {
            KlantViewModel klantViewModel = new KlantViewModel()
            {
                Huisnummer = bestelling.Klantgegevens?.Huisnummer,
                Land = bestelling.Klantgegevens?.Land,
                Naam = bestelling.Klantgegevens?.Naam,
                Postcode = bestelling.Klantgegevens?.Postcode,
                Straatnaam = bestelling.Klantgegevens?.Straatnaam,
                Woonplaats = bestelling.Klantgegevens?.Woonplaats
            };
            List<Models.FactuurViewModels.BestellingRegelViewModel> bestelregels = new List<Models.FactuurViewModels.BestellingRegelViewModel>();
            foreach (var item in bestelling.Bestelregels)
            {
                Models.FactuurViewModels.BestellingRegelViewModel regel = new Models.FactuurViewModels.BestellingRegelViewModel()
                {
                    AantalArtikelen = item.AantalArtikelen,
                    ArtikelId = item.ArtikelId,
                    ArtikelNaam = item.ArtikelNaam,
                    PrijsPerArtikelInc = item.PrijsPerArtikelInc
                };
                bestelregels.Add(regel);
            }
            FactuurViewModel viewModel = new FactuurViewModel()
            {
                KlantViewModel = klantViewModel,
                BestellingRegelViewModels = bestelregels,
                Bestelnummer = bestelling.Bestelnummer,
                TotaalBedragExc = bestelling.TotaalBedragExc,
                TotaalBedragInc = bestelling.TotaalBedragInc
            };
            return viewModel;
        }

        public IActionResult PickBestellingAndContinue(int bestelnummer)
        {
            try
            {
                updateVoorraad(bestelnummer);

                PickBestelling(bestelnummer);
                _logger.LogInformation($"Bestelling with bestelnummer: {bestelnummer} has the status 'picked'");
            }
            catch (KeyNotFoundException exception)
            {
                _logger.LogError(exception.Message);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"An unexpected error occured in IntranetController.PickBestellingAndContinue, bestelnummer: {bestelnummer} \n Errormessage: {e.Message} \n Stacktrace: {e.StackTrace}");
                return View("Oeps");
            }
            return Index();
        }

        private void updateVoorraad(int bestelnummer)
        {
            Bestelling bestelling = _bestellingService.GetBestelling(bestelnummer);
            foreach (Bestelregel regel in bestelling.Bestelregels)
            {
                HaalArtikelUitMagazijnCommand command = new HaalArtikelUitMagazijnCommand
                {
                    Aantal = regel.AantalArtikelen,
                    ArtikelID = (int)regel.ArtikelId
                };
                _magazijnBeheerAgent.SendHaalArtikelUitMagazijnCommand(command);
            }
        }

        

        public IActionResult PickBestellingAndPause(int bestelnummer)
        {
            try
            {
                PickBestelling(bestelnummer);
                _logger.LogInformation($"Bestelling with bestelnummer: {bestelnummer} has the status 'picked'");
            }
            catch (KeyNotFoundException exception)
            {
                _logger.LogError(exception.Message);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"An unexpected error occured in IntranetController.PickBestellingAndPause, bestelnummer: {bestelnummer} \n Errormessage: {e.Message} \n Stacktrace: {e.StackTrace}");
                return View("Oeps");
            }
            return Pause();
        }

        private void PickBestelling(int bestelnummer)
        {
            _bestellingService.PickBestelling(bestelnummer);
        }
    }
}