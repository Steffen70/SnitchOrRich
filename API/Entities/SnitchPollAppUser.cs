namespace API.Entities
{
    public class SnitchPollAppUser
    {
        public SnitchPoll SnitchPoll { get; set; }
        public int SnitchPollId { get; set; }

        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}