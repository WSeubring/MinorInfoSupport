using Lapiwe.GMS.FrontEnd.DAL.Interfaces;
using Lapiwe.GMS.FrontEnd.Enitities;
using Lapiwe.GMS.FrontEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.GMS.FrontEnd.Controllers
{
    public class KlantController : Controller
    {
        private IKlantRepository _klantRepository;

        public KlantController(IKlantRepository klantRepository)
        {
            _klantRepository = klantRepository;
        }

        [HttpGet]
        public IActionResult Details(long id)
        {
            _klantRepository.Insert(null);

            var klant = _klantRepository.Find(id);
            var viewmodel = new KlantGegegevensViewModel();

            viewmodel.Klant = klant;
            
            return View(viewmodel);
        }
    }
}
