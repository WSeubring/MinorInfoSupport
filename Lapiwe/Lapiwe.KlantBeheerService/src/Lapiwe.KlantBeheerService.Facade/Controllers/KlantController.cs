using Lapiwe.Common.Infastructure;
using Lapiwe.KlantBeheerService.Domain;
using Lapiwe.KlantBeheerService.Export;
using Lapiwe.KlantBeheerService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Collections.Generic;
using System.Net;

namespace Lapiwe.KlantBeheerService.Facade.Controllers
{
    [Route("api/v1/[controller]")]
    public class KlantController : Controller
    {
        private IRepository _repository;
        private IEventPublisher _publisher;

        public KlantController(IRepository repo, IEventPublisher pub)
        {
            _repository = repo;
            _publisher = pub;
        }

        // POST api/klant
        [SwaggerOperation("MaakNieuweKlant")]
        [HttpPost]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult MaakNieuweKlant([FromBody]MaakNieuweKlantCommand command)
        {
            if (ModelState.IsValid && command != null)
            {
                var klant = new Klant(
                    voornaam: command.Voornaam,
                    tussenvoegsel: command.Tussenvoegsel,
                    achternaam: command.Achternaam,
                    adres: command.Adres,
                    postcode: command.Postcode,
                    woonplaats: command.Woonplaats,
                    telefoonummer: command.Telefoonnummer,
                    email: command.Email
                );
                _repository.Insert(klant);

                var klantAangemaaktEvent = new KlantIsAangemaaktEvent(
                    klantGuid: klant.Guid,
                    voornaam: command.Voornaam,
                    tussenvoegsel: command.Tussenvoegsel,
                    achternaam: command.Achternaam,
                    adres: command.Adres,
                    postcode: command.Postcode,
                    woonplaats: command.Woonplaats,
                    telefoonummer: command.Telefoonnummer,
                    email: command.Email
                );
                _publisher.Publish(klantAangemaaktEvent);

                return Ok();
            }

            return BadRequest();
        }
    }

}