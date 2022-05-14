using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Todo : DomainEntityId
{
    public string TodoText { get; set; } = default!;
    
    public bool IsDone { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    
}