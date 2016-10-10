using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL.Interfaces;
using Enities;
using Swashbuckle.SwaggerGen.Annotations;

namespace MInor.Dag18.FrontEndOprachtApi.Controllers
{
    [Route("api/[controller]")]
    public class MonumentController : Controller
    {
        private IRepository<Monument, int> _monumentRepository;

        public MonumentController(IRepository<Monument, int> monumentRepository)
        {
            _monumentRepository = monumentRepository;
        }

        [HttpGet]
        [SwaggerOperation("Get")]
        public IEnumerable<Monument> Get()
        {
            return _monumentRepository.FindAll();
        }

        [HttpGet("{id}")]
        [SwaggerOperation("GetById")]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_monumentRepository.FindByKey(id));
            }
            catch (Exception)
            {
                var error = new FunctionalError("US001", "Monument is niet gevonden.");
                return NotFound(error);
            }
        }

        [HttpPost]
        [SwaggerOperation("Add")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult Post([FromBody]Monument item)
        {
            try
            {
                _monumentRepository.Add(item);
                return Ok();
            }
            catch (ArgumentException)
            {
                var error = new FunctionalError("US003", "Monument kan niet worden toegevoegd.");
                return BadRequest(error);
            }
        }

       
        [HttpDelete("{id}")]
        [SwaggerOperation("Delete")]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(OkResult), 200)]
        public IActionResult Delete(int id)
        {
            try
            {
                _monumentRepository.Delete(id);
                return Ok();
            }
            catch (ArgumentException)
            {
                var error = new FunctionalError("US002", "Monument kon niet worden verwijderd");
                return NotFound(error);
            }
        }
    }
}
