using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lapiwe.GMS.FrontEnd.Controllers
{
    public class KlantController : Controller
    {
        public IActionResult Inschrijven()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registreer([FromBody] object ViewModelKlant)
        {

            return RedirectToAction("Index");
        }
    }
}
