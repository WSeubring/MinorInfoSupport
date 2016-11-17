namespace Minor.Dag39.SpellenOefening.FronendService.Domain.Models
{
    public class Player
    {
        public string Name { get;private set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}