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
        [HttpGet("Cursus/Jaar/{jaar}/Week/{week}")]
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Importeren()
        {
            var model = new ImporterenViewModel();
            return View(model);
        }

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