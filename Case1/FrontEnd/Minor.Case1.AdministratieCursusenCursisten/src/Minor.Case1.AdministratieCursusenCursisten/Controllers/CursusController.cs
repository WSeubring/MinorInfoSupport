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

namespace Minor.Case1.AdministratieCursusenCursisten.Controllers
{
    public class CursusController : Controller
    {
        private ICursusInstantieAgentWrapper _cursusInstantieAgent;

        public CursusController(ICursusInstantieAgentWrapper cursusInstantieAgent)
        {
            _cursusInstantieAgent = cursusInstantieAgent;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new List<CursusOverzicht>();
            foreach (var item in _cursusInstantieAgent.Get().OrderBy(ci => ci.StartDatum))
            {
                model.Add(new CursusOverzicht()
                {
                    AantalDeelNemers = 1,
                    CursusInstantie = item
                });

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Importeren()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Importeren(IFormFile file)
        {
            using (var streamReader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
            {
                var text = streamReader.ReadToEnd();

                _cursusInstantieAgent.AddFromTextFile(text);
            }
            return View();
        }
    }
}