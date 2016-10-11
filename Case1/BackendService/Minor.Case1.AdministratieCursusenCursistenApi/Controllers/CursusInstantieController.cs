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

namespace Minor.Case1.AdministratieCursusenCursistenApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CursusInstantie")]
    public class CursusInstantieController : Controller
    {
        private IRepository<CursusInstantie, long> _cursusInstantieRepository;
     
        public CursusInstantieController(IRepository<CursusInstantie, long> cursusInstantieRepository)
        {
            _cursusInstantieRepository = cursusInstantieRepository;
        }

        [HttpGet]
        [SwaggerOperation("Get")]
        public IEnumerable<CursusInstantie> Get()
        {
            return _cursusInstantieRepository.FindAll();
        }

        [HttpPost]
        [SwaggerOperation("AddFromTextFile")]
        public IActionResult AddFromTextFile(string text)
        {
            int nAddedCursussen = 0;
            int nDuplicates= 0;

            List<CursusInstantie> cursusInstanties = new CursusTextParser().Parse(text);


            return Ok(new { nAddedCursussen, nDuplicates });
        }

    }
}