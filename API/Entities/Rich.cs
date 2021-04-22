using System;

namespace API.Entities
{
    public class Rich : Chore
    {
        public int Points { get; set; }
        public DateTime? Completed { get; set; }
        public Snitch Snitch { get; set; }

        public bool IsIncomplete => this.Creator is null;
        public bool IsExpired => Snitch?.Created.AddHours(Family.SnitchDeadlineHours) > Completed;
        public int Multiplier => (int)Math.Floor(((Completed ?? DateTime.UtcNow) - Snitch.Created).TotalHours);
    }
}