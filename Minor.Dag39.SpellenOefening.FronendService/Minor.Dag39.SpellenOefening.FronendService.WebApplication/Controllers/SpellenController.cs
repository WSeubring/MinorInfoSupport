using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minor.Dag39.SpellenOefening.FronendService.Domain.Services;
using Minor.Dag39.SpellenOefening.FronendService.Facade.ViewModels;
using Minor.Dag39.SpellenOefening.FronendService.Domain.Models;

namespace Minor.Dag39.SpellenOefening.FronendService.WebApplication.Controllers
{
    public class SpellenController : Controller
    {
        private IGameService _gameservice;
        public SpellenController(IGameService gameservice)
        {
            _gameservice = gameservice;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(StartGameViewModel viewmodel)
        {
            Game game = new Game()
            {
                Players = new List<Player>()
                {
                    new Player(viewmodel.Player1),
                    new Player(viewmodel.Player2),
                    new Player(viewmodel.Player3)
                }
            };

            game = _agent.StartGame(game);

            return RedirectToAction("PlayGame", new { id = game.ID });
        }

        [HttpGet]
        public IActionResult PlayGame(int id)
        {
            var game = _agent.GetGameById(id);

            return View(new PlayGameViewModel(game));
        }

        [HttpPost]
        [ActionName("PlayGame")]
        public IActionResult PlayGamePost(int id)
        {
            var game = _agent.GetGameById(id);
            game = _agent.PlayGame(game);

            return View(new PlayGameViewModel(game));
        }
    }
}
