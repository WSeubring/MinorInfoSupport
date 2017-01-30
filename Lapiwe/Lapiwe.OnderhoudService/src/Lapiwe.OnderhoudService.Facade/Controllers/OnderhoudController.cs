
using Lapiwe.Common.Domain;
using Lapiwe.Common.Infastructure;
using Lapiwe.OnderhoudService.Domain;
using Lapiwe.OnderhoudService.Export;
using Lapiwe.OnderhoudService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System;
using Swashbuckle.SwaggerGen.Annotations;

namespace Lapiwe.OnderhoudService.Facade.Controllers
{
    [Route("api/v1/[controller]")]
    public class OnderhoudController : Controller
    {
        private IRepository repository;
        private IEventPublisher publisher;

        public OnderhoudController(IRepository repo, IEventPublisher pub)
        {
            repository = repo;
            publisher = pub;
        }

        // POST api/onderhoud/aanmelden
        [SwaggerOperation("MaakNieuwOnderhoudsOpdracht")]
        [Route("aanmelden")]
        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public IActionResult MaakNieuwOnderhoudsOpdracht([FromBody]MeldOnderhoudsOpdrachtAanCommand command)
        {
            if (ModelState.IsValid && command != null)
            {
                OnderhoudsOpdracht opdracht = new OnderhoudsOpdracht
                {
                    AanmeldDatum = command.AanmeldDatum,
                    Apk = command.Apk,
                    AutoGuid = command.AutoGuid,
                    Kilometerstand = command.Kilometerstand,
                    KlantGuid = command.KlantGuid,
                    OpdrachtOmschrijving = command.OpdrachtOmschrijving
                };

                repository.Insert(opdracht);

                var OnderhoudEvent = new OnderhoudsOpdrachtAangemeldEvent
                {
                    AanmeldDatum = opdracht.AanmeldDatum,
                    Apk = opdracht.Apk,
                    AutoGuid = opdracht.AutoGuid,
                    Kilometerstand = opdracht.Kilometerstand,
                    KlantGuid = opdracht.KlantGuid,
                    OnderhoudsOpdrachtGuid = opdracht.Guid,
                    OpdrachtOmschrijving = opdracht.OpdrachtOmschrijving
                };

                publisher.Publish(OnderhoudEvent);
                return Ok();
            }

            return BadRequest();
        }

        [SwaggerOperation("StartNieuwOnderhoudsOpdracht")]
        [Route("start")]
        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public IActionResult StartNieuwOnderhoudsOpdracht([FromBody]StartOnderhoudOpdrachtCommand command)
        {
            if (ModelState.IsValid && command != null)
            {
                var onderhoudsOpdracht = repository.Find(command.OnderhoudOpdrachtGuid);
                onderhoudsOpdracht.OpdrachtStatus = Status.Onderhoud;
                repository.Update(onderhoudsOpdracht);

                var onderhoudsOpdrachtGestartEvent = new OnderhoudsOpdrachtGestartEvent(command.OnderhoudOpdrachtGuid);
                publisher.Publish(onderhoudsOpdrachtGestartEvent);

                return Ok();
            }
            return BadRequest();
        }
    }

}