using Agents;
using Microsoft.AspNetCore.Mvc;
using Minor.Dag18.FrontEndOpdracht.Models;
using System.Collections.Generic;
using System;

namespace Minor.Dag18.FrontEndOpdracht.Controllers
{
    public class MonumentenController : Controller
    {
        private IMonumentenAgent _monumentAgent;

        public MonumentenController(IMonumentenAgent monumentenAgent)
        {
            _monumentAgent = monumentenAgent;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = _monumentAgent.GetAllMonumenten();

            return View(model);
        }


        public IActionResult Delete(int? id)
        {
            if(id != null)
            {
                _monumentAgent.Delete((int)id);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {

           return View();
        }
    }
}
