using System;

namespace API.Entities
{
    public class Snitch : IChore
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public AppUser Creator { get; set; }
        public int CreatorId { get; set; }
        public AppUser Target { get; set; }
        public int? TargetId { get; set; }
        public Photo Photo { get; set; }
        public int PhotoId { get; set; }
        public Family Family { get; set; }
        public Guid FamilyId { get; set; }

        public Rich Rich { get; set; }
        public int RichId { get; set; }

        public SnitchPoll SnitchPoll { get; set; }
    }
}