using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minor.Case1.AdministratieCursusenCursisten.ViewModels;
using Minor.Case1.AdministratieCursusenCursisten.Agents;
using Minor.Case1.AdministratieCursusenCursisten.Agents.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Globalization;

namespace Minor.Case1.AdministratieCursusenCursisten.Controllers
{
    public class CursusController : Controller
    {
        private ICursusInstantieAgentWrapper _cursusInstantieAgent;
        private DateTimeFormatInfo _dfi = new DateTimeFormatInfo();

        public CursusController(ICursusInstantieAgentWrapper cursusInstantieAgent)
        {
            _cursusInstantieAgent = cursusInstantieAgent;
        }
        /// <summary>
        /// Redirects to index with the current week and year as parameter.
        /// </summary>
        /// <returns></returns>
        [Route("CAS_WS/CASsite")]
        [HttpGet]
        public RedirectToActionResult CurrentWeekRedirect()
        {
            var calendar = _dfi.Calendar;

            return RedirectToAction("Index",
                new {
                    jaar = DateTime.Now.Year,
                    week = calendar.GetWeekOfYear(DateTime.Now, _dfi.CalendarWeekRule, _dfi.FirstDayOfWeek)
                });
        }
        /// <summary>
        /// Show a overview of the CursusInstanties within the given week and year.
        /// </summary>
        /// <param name="jaar"></param>
        /// <param name="week"></param>
        /// <returns></returns>
        [Route("CAS_WS/CASsite/Cursus")]
        [HttpGet("Jaar/{jaar}/Week/{week}")]
        public IActionResult Index([Bind("jaar", "week")]int jaar, int week)
        {
            var model = new CursusOverzichtViewModel();
            if(week > 52)
            {
                week = 1;
                jaar++;
                return RedirectToAction("Index", new { jaar = jaar, week = week });
            }
            if(week < 1)
            {
                week = 52;
                jaar--;
                return RedirectToAction("Index", new { jaar = jaar, week = week });
            }

            model.Jaar = jaar;
            model.Weeknr = week;

            model.CursusInstanties = _cursusInstantieAgent.Get(jaar, week);

            return View(model);
        }
        /// <summary>
        /// Show a view where a file can be uploaded to import CursusInstanties
        /// </summary>
        /// <returns></returns>
        [Route("CAS_WS/CASsite/Importeren")]
        [HttpGet]
        public IActionResult Importeren()
        {
            var model = new ImporterenViewModel();
            return View(model);
        }

        /// <summary>
        /// Converts the uploaded file to text sends it to the API to be converted to CursusInstanties and added to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("CAS_WS/CASsite/Importeren")]
        [HttpPost]
        public IActionResult Importeren(ImporterenViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var streamReader = new StreamReader(model.File.OpenReadStream(), Encoding.UTF8))
                {
                    var text = streamReader.ReadToEnd();

                    model.Report = _cursusInstantieAgent.AddFromTextFile(text);
                }
            }
            return View(model);
        }
    }
}