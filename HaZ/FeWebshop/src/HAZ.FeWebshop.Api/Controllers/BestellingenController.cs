using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class BestellingenController : Controller
    {
        private IBestellingService _bestellingService;

        private ILogger<BestellingenController> _logger;

        public BestellingenController(ILogger<BestellingenController> logger, IBestellingService bestellingService)
        {
            _logger = logger;
            _bestellingService = bestellingService;
        }

        [HttpPost]
        public ActionResult PlaatsBestelling([FromBody] Bestelling bestelling)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("BestellingController.PlaatsBestelling: Bestelling wordt geplaatst");
                    BestellingResult result = _bestellingService.PlaatsBestelling(bestelling);
                    if (result.IsValid)
                    {
                        PathString pathString = Request?.Path ?? "/home";
                        FullBestelling fullBestelling = result.Bestelling;
                        return Created(pathString, fullBestelling);
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return BadRequest(allErrors);
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical($"An error occured in BestellingenController.Plaatsbestelling: {e.Message} /r StackTrace: {e.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}
