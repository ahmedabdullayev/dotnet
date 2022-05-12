using Base.Domain;

namespace App.Public.DTO.v1;

public class Topic : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public ICollection<UserPost>? UserPosts { get; set; }
}