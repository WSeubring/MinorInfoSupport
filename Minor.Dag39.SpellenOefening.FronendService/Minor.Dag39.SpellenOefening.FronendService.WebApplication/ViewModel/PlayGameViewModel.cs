using Minor.Dag39.SpellenOefening.FronendService.Domain.Models;

namespace Minor.Dag39.SpellenOefening.FronendService.Facade.ViewModels
{
    public class PlayGameViewModel
    {
        public Game Game { get; set; }

        public PlayGameViewModel()
        {
        }
        public PlayGameViewModel(Game game)
        {
            Game = game;
        }
    }
}