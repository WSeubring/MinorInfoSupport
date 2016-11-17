using Commands;
using Microsoft.AspNetCore.Mvc;
using Minor.SpellenOefening.GameAdministrationService.Domain.Services.Interfaces;
using System;

namespace Services
{
    public class GameService : IGameService
    {
        public GameService(IGameRepository repo, IEventPublisher publisher)
        {

        }
        public void PlayGame(PlayGameCommand playGameCommand)
        {

        }

        public void StartGame(StartGameCommand sgc)
        {
            
        }
    }
}