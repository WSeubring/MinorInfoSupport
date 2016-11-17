using Commands.Entities;
using System.Collections.Generic;

namespace Commands
{
    public class StartGameCommand
    {
        public List<StartGamePlayer> Players { get; set; }
    }
}