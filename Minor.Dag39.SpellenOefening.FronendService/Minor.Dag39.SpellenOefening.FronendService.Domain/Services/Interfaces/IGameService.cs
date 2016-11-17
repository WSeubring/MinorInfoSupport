using Minor.Dag39.SpellenOefening.FronendService.Domain.Models;

namespace Minor.Dag39.SpellenOefening.FronendService.Domain.Services
{
    public interface IGameService
    {
        Game StartGame(Game game);
        Game PlayGame(Game game);
        Game GetGameById(int id);
    }
}