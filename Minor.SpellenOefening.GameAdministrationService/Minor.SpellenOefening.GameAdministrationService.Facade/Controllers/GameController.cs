using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Commands;
using Minor.SpellenOefening.GameAdministrationService.Domain.Services.Interfaces;

namespace Minor.SpellenOefening.GameAdministrationService.Facade.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET api/values
        [HttpGet]
        public IActionResult StartGame(StartGameCommand sgc)
        {
            _gameService.StartGame(sgc);
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public IActionResult PlayGame(PlayGameCommand pgc)
        {
            _gameService.PlayGame(pgc);
            return Ok();
        }
    }
}
