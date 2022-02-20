using App.Domain.Identity;
using Base.Domain;

namespace App.Domain.Posts;

public class UserPost : DomainEntityMetaId
{
    public string Title { get; set; } = default!;
    public string Text { get; set; } = default!;

    public Guid TopicId { get; set; }
    public Topic? Topic { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}