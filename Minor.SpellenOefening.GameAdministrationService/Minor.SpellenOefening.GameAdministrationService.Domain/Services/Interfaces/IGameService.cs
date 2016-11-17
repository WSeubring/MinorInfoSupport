using Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.SpellenOefening.GameAdministrationService.Domain.Services.Interfaces
{
    public interface IGameService
    {
        void StartGame(StartGameCommand sgc);
        void PlayGame(PlayGameCommand playGameCommand);
    }
}
