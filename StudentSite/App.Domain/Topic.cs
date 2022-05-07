using Base.Domain;

namespace App.Domain;

public class Topic : DomainEntityMetaId
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public ICollection<UserPost>? UserPosts { get; set; }
}