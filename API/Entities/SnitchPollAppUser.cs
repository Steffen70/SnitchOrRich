using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("SnitchPollAppUsers")]
    public class SnitchPollAppUser
    {
        public SnitchPoll SnitchPoll { get; set; }
        public int SnitchPollId { get; set; }

        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}