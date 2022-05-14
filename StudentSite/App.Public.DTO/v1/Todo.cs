using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Todo : DomainEntityId
{
    public string TodoText { get; set; } = default!;
    
    public bool IsDone { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    
}