using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Quiz : DomainEntityMetaId
{
    [Column(TypeName = "jsonb")] // convert to json and save as string, and when we get it deserialize it and return object
    public LangStr Name { get; set; } = new();
    [Column(TypeName = "jsonb")] // convert to json and save as string, and when we get it deserialize it and return object
    public LangStr Description { get; set; } = new();


    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    // public Guid AppUserId { get; set; }
    // public AppUser? AppUser { get; set; }

    public ICollection<Question>? Questions { get; set; }
    public ICollection<UserChoice>? UserChoices { get; set; }

}