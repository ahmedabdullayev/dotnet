using System.ComponentModel.DataAnnotations;
using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Quiz : DomainEntityId
{
    [MaxLength(30)]
    public string Name { get; set; } = default!;
    [MaxLength(255)]
    public string Description { get; set; } = default!;

    public bool IsReady { get; set; }

    public Guid SubjectId { get; set; } 
    // public Subject? Subject { get; set; }


    public ICollection<Question>? Questions { get; set; }
    public ICollection<UserChoice>? UserChoices { get; set; }

}