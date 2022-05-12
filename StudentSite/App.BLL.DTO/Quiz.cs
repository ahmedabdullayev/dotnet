using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Quiz : DomainEntityId
{
    [MaxLength(30)]
    public string Name { get; set; } = default!;
    [MaxLength(255)]
    public string Description { get; set; } = default!;

    public bool IsReady { get; set; }

    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    // public Guid AppUserId { get; set; }
    // public AppUser? AppUser { get; set; }

    public ICollection<Question>? Questions { get; set; }
    public ICollection<UserChoice>? UserChoices { get; set; }

}