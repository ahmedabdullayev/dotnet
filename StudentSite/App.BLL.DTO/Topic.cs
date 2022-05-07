using Base.Domain;

namespace App.BLL.DTO;

public class Topic : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public ICollection<UserPost>? UserPosts { get; set; }
}