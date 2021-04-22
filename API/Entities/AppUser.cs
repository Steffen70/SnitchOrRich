using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public Family Family { get; set; }
        public Guid FamilyId { get; set; }

        public string UserRole { get; set; } = "Member";

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        
        public ICollection<SnitchPollAppUser> SnitchPollAppUsers { get; set; }
    }
}