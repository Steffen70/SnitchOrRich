using System;

namespace API.Entities
{
    public class Rich : IChore
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

        public int Points { get; set; } = 0;
        public DateTime? Completed { get; set; }
        public Snitch Snitch { get; set; }

        public bool IsIncomplete => Creator is null;
        public bool IsExpired => Snitch.Created.AddHours(Family.SnitchDeadlineHours) > (Completed ?? DateTime.UtcNow);
        public int Multiplier => (int)Math.Floor(((Completed ?? DateTime.UtcNow) - Snitch.Created).TotalHours);
    }
}