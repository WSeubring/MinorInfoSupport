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

        [HttpGet]
        public IActionResult StartGame(StartGameCommand sgc)
        {
            _gameService.StartGame(sgc);
            return Ok();
        }

        public IActionResult PlayGame(PlayGameCommand pgc)
        {
            _gameService.PlayGame(pgc);
            return Ok();
        }
    }
}
