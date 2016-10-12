using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minor.Case1.AdministratieCursusenCursistenApi.Entiteiten;
using Minor.Case1.AdministratieCursusenCursistenApi.DAL.Interfaces;
using Swashbuckle.SwaggerGen.Annotations;
using Minor.Case1.AdministratieCursusenCursistenApi.Services;
using Minor.Case1.AdministratieCursusenCursistenApi.Exceptions;

namespace Minor.Case1.AdministratieCursusenCursistenApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CursusInstantie")]
    public class CursusInstantieController : Controller
    {
        private IRepository<CursusInstantie, long> _cursusInstantieRepository;
        private ICursusTextParser _cursusTextParser;

        public CursusInstantieController(IRepository<CursusInstantie, long> cursusInstantieRepository, ICursusTextParser cursusTextParser)
        {
            _cursusInstantieRepository = cursusInstantieRepository;
            _cursusTextParser = cursusTextParser;
        }

        [HttpGet]
        [SwaggerOperation("Get")]
        public IEnumerable<CursusInstantie> Get()
        {
            return _cursusInstantieRepository.FindAll();
        }

        [HttpPost]
        [SwaggerOperation("AddFromTextFile")]
        [ProducesResponseType(typeof(OkResult), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public IActionResult AddFromTextFile([FromBody]string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                List<CursusInstantie> cursusInstanties = new List<CursusInstantie>();
                var addFromFileResultReport = new AddFromFileResultReport();
                try
                {
                    cursusInstanties = _cursusTextParser.Parse(text);

                    var allCursusInstanties = _cursusInstantieRepository.FindAll();
                    var nonDuplicateCursusInstanties = cursusInstanties.Except(allCursusInstanties, new CursusInstantieComparer());
                    _cursusInstantieRepository.AddRange(nonDuplicateCursusInstanties);

                    addFromFileResultReport.AantalInsertedCursusInstanties = nonDuplicateCursusInstanties.Count();
                    addFromFileResultReport.AantalDuplicatesInFile = cursusInstanties.Count() - nonDuplicateCursusInstanties.Count(); ;

                    return Ok("{ nAddedItems:" + nAddedItems + ", nDuplicateItems:" + nDuplicateItems + " }");
                }
                catch (InvalidSyntaxException ex)
                {
                    addFromFileResultReport.HasSyntaxError = true;
                    addFromFileResultReport.LineOfSyntaxError = int.Parse(ex.Message);
                }
            }
            return BadRequest();
        }
    }
}