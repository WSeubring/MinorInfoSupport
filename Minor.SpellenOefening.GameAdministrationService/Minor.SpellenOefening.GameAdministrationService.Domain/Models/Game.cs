using System.Collections.Generic;

namespace Models
{
    public class Game
    {
        public int ID { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
    }
}