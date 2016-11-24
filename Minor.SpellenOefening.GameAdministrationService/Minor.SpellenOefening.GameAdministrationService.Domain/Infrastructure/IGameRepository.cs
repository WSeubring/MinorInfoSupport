using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.SpellenOefening.GameAdministrationService.Domain.Infrastructure
{
    public interface IGameRepository
    {
        /// <summary>
        /// Adds a game to the repository
        /// </summary>
        /// <param name="game"></param>
        
        void Add(Game game);
        Game FindByID(int gameID);
        void Update(Game game);
    }
}
