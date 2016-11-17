using System.Collections.Generic;

namespace Minor.Dag39.SpellenOefening.FronendService.Domain.Models
{
    public class Game
    {
        public int ID { get; set; }
        public List<Player> Players { get; set; }
        public Player Winner { get; set; }
    }
}