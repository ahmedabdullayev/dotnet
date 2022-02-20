using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Todo : DomainEntityMetaId
{
    public string TodoText { get; set; } = default!;
    
    public DateTime? Deadline { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}