using Lapiwe.GMS.FrontEnd.DAL;
using Lapiwe.GMS.FrontEnd.Entities;
using Lapiwe.GMS.FrontEnd.ViewModels;
using Lapiwe.IS.RDW.Export.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lapiwe.GMS.FrontEnd.Agents;
using Lapiwe.GMS.FrontEnd.Agents.Models;

namespace Lapiwe.GMS.FrontEnd.Controllers
{
    [Route("[controller]")]
    public class OnderhoudsOpdrachtenController : Controller
    {
        private ISimpleRepository _repository;
        private IOnderhoudsServiceAgent _agent;

        public OnderhoudsOpdrachtenController(IOnderhoudsServiceAgent agent, ISimpleRepository repository)
        {
            _agent = agent;
            _repository = repository;
        }

        // Default route, so localhost:xxxx/Onderhoud will show this method
        [HttpGet]
        public IActionResult Overzicht()
        {
            IEnumerable<OnderhoudsOpdracht> onderhoudsOpdrachten = _repository.AlleOnderhoudsOpdrachten();

            OnderhoudsOpdrachtenViewModel model = new OnderhoudsOpdrachtenViewModel(onderhoudsOpdrachten);

            return View(model);
        }

        [Route("Nieuw")]
        [HttpGet]
        public IActionResult Invullen()
        {
            return View();
        }

        [Route("Nieuw")]
        [HttpPost]
        public IActionResult Toevoegen(
            string klantnaam, string telefoonnummer, string kenteken, 
            int kilometerstand, string opdrachtomschrijving, bool apk
        ) {
            Auto auto = new Auto(kenteken, kilometerstand);
            Klant klant = new Klant(klantnaam, telefoonnummer);

            MeldOnderhoudsOpdrachtAanCommand command = new MeldOnderhoudsOpdrachtAanCommand(
                klantGuid: klant.Guid,
                autoGuid: auto.Guid,
                aanmeldDatum: DateTime.Now,
                kilometerstand: kilometerstand,
                opdrachtOmschrijving: opdrachtomschrijving,
                apk: apk
            );

            command.CorrelationID = Guid.NewGuid();
            command.TimeStamp = DateTime.Now;

            _repository.Add(auto);
            _repository.Add(klant);

            _agent.MaakNieuwOnderhoudsOpdracht(command);

            return RedirectToAction("Overzicht");
        }

        [Route("Zoek")]
        [HttpGet]
        public IActionResult Zoek()
        {
            return View();
        }

        [Route("{kenteken}")]
        [HttpGet]
        public IActionResult OpdrachtDetail(string kenteken)
        {
            return View();
        }

        [Route("{kenteken}")]
        [HttpPost]
        public IActionResult OpdrachtUpdate(Guid opdrachtGuid, string werkzaamheden)
        {
            // TODO: update 
            var kenteken = "test";
            return RedirectToAction("OpdrachtDetail", new { kenteken = kenteken } );
        }
    }
}
