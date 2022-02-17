using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace Base.Domain;

public abstract class DomainEntityMetaId : DomainEntityMetaId<Guid>, IDomainEntityId
{
}

public abstract class DomainEntityMetaId<TKey> : DomainEntityId<TKey>, IDomainEntityMeta
    where TKey : IEquatable<TKey>
{
    [ScaffoldColumn(false)]
    [MaxLength(32)]
    public string? CreatedBy { get; set; } 
    [ScaffoldColumn(false)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [ScaffoldColumn(false)]
    [MaxLength(32)]
    public string? UpdatedBy { get; set; } 
    [ScaffoldColumn(false)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}