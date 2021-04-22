namespace API.Entities
{
    public class Snitch : Chore
    {
        public AppUser Target { get; set; }
        public int TargetId { get; set; }

        public Rich Rich { get; set; }
        public int RichId { get; set; }

        public SnitchPoll SnitchPoll { get; set; }
    }
}