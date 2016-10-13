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
using System.Linq.Expressions;
using System.Globalization;

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

        /// <summary>
        /// Gets all the CursusInstanties in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation("Get")]
        public IEnumerable<CursusInstantie> Get()
        {
            return _cursusInstantieRepository.FindAll();
        }

        /// <summary>
        /// Add the Cursusinstaties from a textfile to the database
        /// </summary>
        /// <param name="text"></param>
        /// <returns>A resultreport of the add action</returns>
        [HttpPost]
        [SwaggerOperation("AddFromTextFile")]
        public AddFromFileResultReport AddFromTextFile([FromBody]string text)
        {
            List<CursusInstantie> cursusInstanties = new List<CursusInstantie>();
            try
            {
                int nAddedItems = 0;
                int nDuplicateItems = 0;
               

                cursusInstanties = _cursusTextParser.Parse(text);
                if (cursusInstanties.Count() > 0)
                {
                    var allCursusInstanties = _cursusInstantieRepository.FindAll();
                    var nonDuplicateCursusInstanties = cursusInstanties.Except(allCursusInstanties, new CursusInstantieComparer());
                    _cursusInstantieRepository.AddRange(nonDuplicateCursusInstanties);

                    nAddedItems = nonDuplicateCursusInstanties.Count();
                    nDuplicateItems = cursusInstanties.Count() - nAddedItems;
                }

                return new AddFromFileResultReport(nAddedItems, nDuplicateItems);
            }
            catch (InvalidSyntaxException ex)
            {
                    return new AddFromFileResultReport(ex.Message);
            }
        }

        /// <summary>
        /// Return a list of CursusInstanties that are active within the range of the given week and year.
        /// </summary>
        /// <param name="jaar"></param>
        /// <param name="week"></param>
        /// <returns>A list of CursusInstanties</returns>
        [HttpGet("{jaar}/{week}")]
        [SwaggerOperation("GetByYearAndWeek")]
        public IEnumerable<CursusInstantie> GetByYearAndWeek(int jaar, int week)
        {
            var _dfi = new DateTimeFormatInfo();
            var calendar = _dfi.Calendar;

            DateTime beginOfWeek = calendar.AddWeeks(new DateTime(jaar, 1, 1), week - 1); //DatetimeBegint op week 1, vandaar min 1 bij de week
            DateTime endOfWeek = calendar.AddWeeks(new DateTime(jaar, 1, 1), week);
            Expression<Func<CursusInstantie, bool>> filter = ci => (ci.StartDatum > beginOfWeek || ci.StartDatum.AddDays(ci.Cursus.Duur) > beginOfWeek) && ci.StartDatum < endOfWeek;

            return _cursusInstantieRepository.FindBy(filter);
        }
    }
}