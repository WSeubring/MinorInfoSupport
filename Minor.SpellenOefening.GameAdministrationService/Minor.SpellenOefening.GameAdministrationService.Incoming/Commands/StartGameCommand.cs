using Commands.Entities;
using System.Collections.Generic;

namespace Commands
{
    public class StartGameCommand
    {
        public List<Player> Players { get; set; }
    }
}