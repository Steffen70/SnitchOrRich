using System;

namespace API.Entities
{
    public class Chore
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }

        public AppUser Creator { get; set; }
        public int CreatorId { get; set; }

        public Photo Photo { get; set; }
        public int PhotoId { get; set; }

        public Family Family { get; set; }
        public Guid FamilyId { get; set; }
    }
}