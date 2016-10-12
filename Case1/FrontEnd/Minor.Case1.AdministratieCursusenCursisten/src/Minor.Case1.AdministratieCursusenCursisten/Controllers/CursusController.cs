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
        public IActionResult Index()
        {
            var model = new CursusOverzichtViewModel();
            var calendar = _dfi.Calendar;
            model.Weeknr = calendar.GetWeekOfYear(DateTime.Now, _dfi.CalendarWeekRule, _dfi.FirstDayOfWeek);
            model.CursusInstanties = _cursusInstantieAgent.Get();
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Importeren()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Importeren(ImporterenViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var streamReader = new StreamReader(model.File.OpenReadStream(), Encoding.UTF8))
                {
                    var text = streamReader.ReadToEnd();

                    _cursusInstantieAgent.AddFromTextFile(text);
                }
            }
            return View(model);
        }
    }
}