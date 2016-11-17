using Commands;
using Eventbus;
using Events;
using Microsoft.AspNetCore.Mvc;
using Minor.SpellenOefening.GameAdministrationService.Domain.Infrastructure;
using Minor.SpellenOefening.GameAdministrationService.Domain.Services.Interfaces;
using Models;
using System;

namespace Services
{
    public class GameService : IGameService
    {
        private IGameRepository _repo;
        private IEventPublisher _publisher;
        private Random _random = new Random();
        public GameService(IGameRepository repo, IEventPublisher publisher)
        {
            _repo = repo;
            _publisher = publisher;
        }
        public void StartGame(StartGameCommand sgc)
        {
            var game = new Game();
            foreach(var startGamePlayer in sgc.Players)
            {
                game.Players.Add(new Player(startGamePlayer.Name));
            }

            _repo.Add(game);

            _publisher.Publish(new GameStartedEvent()
            {
                GameID = game.ID,
                Players = sgc.Players
            });
        }
        public void PlayGame(PlayGameCommand playGameCommand)
        {
            var game = _repo.FindByID(playGameCommand.GameID);

            var randomIndex = _random()
        }
    }
}