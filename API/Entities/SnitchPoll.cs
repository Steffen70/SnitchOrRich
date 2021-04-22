using System.Collections.Generic;
using System.Linq;

namespace API.Entities
{
    public class SnitchPoll
    {
        public int Id { get; set; }

        public Snitch Snitch { get; set; }
        public int SnitchId { get; set; }

        public ICollection<SnitchPollVote> Votes { get; set; }

        public IEnumerable<string> PollOptions => Snitch.Family.Members.Select(u => u.Username);
        public int RecommendedPoints => Snitch.Rich.Points;
    }
}