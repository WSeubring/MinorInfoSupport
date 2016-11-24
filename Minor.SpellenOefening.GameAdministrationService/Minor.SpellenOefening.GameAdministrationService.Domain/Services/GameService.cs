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
        private Random _random;

        private IGameRepository _repo;
        private IEventPublisher _publisher;

        public GameService(IGameRepository repo, IEventPublisher publisher)
        {
            _repo = repo;
            _publisher = publisher;

            _random = new Random();
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

            var randomIndex = _random.Next(0, game.Players.Count - 1);

            game.Winner = game.Players[randomIndex];

            _repo.Update(game);

            _publisher.Publish(new GamePlayedEvent()
            {
                GameID = game.ID,
                WinnerName = game.Winner.Name
            });
        }
    }
} 