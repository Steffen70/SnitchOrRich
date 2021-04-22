using System;
using System.Collections.Generic;

namespace API.Entities
{
   public class Family
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int SnitchDeadlineHours { get; set; } = 24;
        public ICollection<AppUser> Members { get; set; }
    }
}