using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class UserPost : DomainEntityId
{
    public string Title { get; set; } = default!;
    public string Text { get; set; } = default!;

    public Guid TopicId { get; set; }
    // public Topic? Topic { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public ICollection<UserComment>? UserComments { get; set; }

}