namespace API.Entities
{
    public class SnitchPollVote
    {
        public int Id { get; set; }
        public int Points { get; set; }

        public SnitchPoll SnitchPoll { get; set; }
        public int SnitchPollId { get; set; }

        public AppUser AppUser { get; set; }
        public AppUser AppUserId { get; set; }
    }
}