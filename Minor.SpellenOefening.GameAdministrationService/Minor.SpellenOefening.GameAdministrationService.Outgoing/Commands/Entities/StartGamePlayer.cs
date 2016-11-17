namespace Commands.Entities
{
    public class StartGamePlayer
    {
        public string Name { get; set; }

        public StartGamePlayer(string name)
        {
            Name = name;
        }
    }
}