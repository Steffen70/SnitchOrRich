using System;

namespace API.Entities
{
    public interface IChore
    {
        int Id { get; set; }
        DateTime Created { get; set; }
        string Description { get; set; }

        AppUser Creator { get; set; }
        int CreatorId { get; set; }

        AppUser Target { get; set; }
        int? TargetId { get; set; }

        Photo Photo { get; set; }
        int PhotoId { get; set; }

        Family Family { get; set; }
        Guid FamilyId { get; set; }
    }
}