using Eventbus;

namespace Events
{
    public class GamePlayedEvent : DomainEvent
    {
        public int GameID { get; set; }
        public string WinnerName { get; set; }
    }
}