using HAZ.FeWebshop.Domain.Entities;
using HAZ.FeWebshop.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Api.Controllers
{
    [Route("api/[controller]")]
    public class ArtikelenController : Controller
    {
        private IArtikelService _artikelService;

        private ILogger<ArtikelenController> _logger;

        public ArtikelenController(ILogger<ArtikelenController> logger, IArtikelService artikelService)
        {
            _logger = logger;
            _artikelService = artikelService;
        }
        // GET api/values
        [HttpGet]
        public IActionResult GetAllArtikelen()
        {
            try
            {
                IEnumerable<Artikel> artikelen = _artikelService.GetAllArtikelen();
                return Json(artikelen);
            }
            catch(Exception e)
            {
                _logger.LogCritical($"An error occured in ArtiklenenController.GetAllArtikelen: {e.Message} /r StackTrace: {e.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}
