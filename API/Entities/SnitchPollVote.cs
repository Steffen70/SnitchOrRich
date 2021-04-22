using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("SnitchPollVotes")]
    public class SnitchPollVote
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public string Username { get; set; }

        public SnitchPoll SnitchPoll { get; set; }
        public int SnitchPollId { get; set; }

        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}