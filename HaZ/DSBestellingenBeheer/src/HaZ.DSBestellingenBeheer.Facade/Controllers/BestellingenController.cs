using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.SwaggerGen.Annotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using HaZ.DSBestellingenBeheer.Facade.Errors;
using Microsoft.Extensions.Logging;
using HaZ.DSBestellingenBeheer.Incoming.Commands;
using HaZ.DSBestellingenBeheer.Services;

namespace HaZ.DSBestellingenBeheer.Facade.Controllers
{
    [Route("api/Bestellingen")]
    public class BestellingenController : Controller
    {
        private ILogger<BestellingenController> _logger;
        private readonly BestellingService _bestellingService;

        public BestellingenController(BestellingService bestellingService, ILogger<BestellingenController> logger)
        {
            _bestellingService = bestellingService;
            _logger = logger;
        }

        [HttpPost]
        [SwaggerOperation("PostBestellingToevoegen")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorMessage), (int)HttpStatusCode.BadRequest)]
        public IActionResult BestellingToevoegen([FromBody]BestellingToevoegenCommand bestellingToevoegenCommand)
        {
            try
            {
                _logger.LogInformation("BestellingController.bestellingToevoegenCommand: BestellingToevoegen");
                using (var bestellingService = _bestellingService)
                {

                    _bestellingService.BestellingToevoegen(bestellingToevoegenCommand);
                    _logger.LogInformation("Bestelling succesvol doorgestuurd naar BestellingService.BestellingToevoegen");
                    return Ok();
                }
            }
            catch (Exception e)
            {
                var error = new ErrorMessage(ErrorType.BadRequest, $"A error occured: {e.Message}");
                _logger.LogCritical(error.FoutMelding);
                return BadRequest(error);
            }
        }

    }
}
