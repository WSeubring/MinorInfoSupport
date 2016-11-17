using System;
using Minor.Dag39.SpellenOefening.FronendService.Domain.Models;
using Minor.Dag39.SpellenOefening.FronendService.Domain.Services;
using System.Collections.Generic;
using Minor.Dag39.SpellenOefening.FronendService.Infrastructure.Repositories;

namespace Minor.Dag39.SpellenOefening.FronendService.Domain.Services
{
    public class GameService : IGameService
    {
        private IGameRepository _repo;

        public Game GetGameById(int id)
        {
            return new Game()
            {
                ID = id,
                Players = new List<Player>()
               {
                   new Player("Mockdata1"),
                   new Player("Mockdata2"),
                   new Player("Mockdata3")
               },
            };
        }

        public Game PlayGame(Game game)
        {
            game.Winner = new Player("Mockdata2");

            return game;
        }

        public Game StartGame(Game game)
        {


            game.ID = 1;
            return game;
        }
    }
}