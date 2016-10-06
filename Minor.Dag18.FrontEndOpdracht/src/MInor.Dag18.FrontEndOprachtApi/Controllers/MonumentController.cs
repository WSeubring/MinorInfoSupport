using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL.Interfaces;
using Enities;

namespace MInor.Dag18.FrontEndOprachtApi.Controllers
{
    [Route("api/[controller]")]
    public class MonumentController : Controller
    {
        private IMonumentenRepository _monumentRepository;

        public MonumentController(IMonumentenRepository monumentRepository)
        {
            _monumentRepository = monumentRepository;
        }

        [HttpGet]
        public IEnumerable<Monument> Get()
        {
            return _monumentRepository.FindAll();
        }

        [HttpGet("{id}")]
        public Monument Get(int id)
        {
            return _monumentRepository.FindById(id);
        }

        [HttpPost]
        public void Post([FromBody]Monument item)
        {
            _monumentRepository.Add(item);
        }

        [HttpDelete("{id}")] 
        public void Delete(int id)
        {
           _monumentRepository.Delete((int)id);
        }
    }
}
