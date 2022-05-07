using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserComment : DomainEntityMetaId
{
    public string CommentText { get; set; } = default!;

    public Guid UserPostId { get; set; }
    public UserPost? UserPost { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}