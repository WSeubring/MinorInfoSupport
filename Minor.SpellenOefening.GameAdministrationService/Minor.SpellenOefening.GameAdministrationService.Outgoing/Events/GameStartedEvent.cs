using Commands.Entities;
using Eventbus;
using System.Collections.Generic;

namespace Events
{
    public class GameStartedEvent : DomainEvent
    {
        public int GameID { get; set; }
        public List<StartGamePlayer> Players { get; set; }
    }
}