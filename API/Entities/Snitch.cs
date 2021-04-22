namespace API.Entities
{
    public class Snitch : Chore
    {
        public Rich Rich { get; set; }
        public int RichId { get; set; }

        public SnitchPoll SnitchPoll { get; set; }
    }
}