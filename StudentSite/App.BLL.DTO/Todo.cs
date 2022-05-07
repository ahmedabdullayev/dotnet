using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Todo : DomainEntityId
{
    public string TodoText { get; set; } = default!;
    
    public DateTime? Deadline { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}