using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class MeetingOption : DomainEntityMetaId
{
    [MaxLength(128)]
    public string Title { get; set; } = default!;

    [MaxLength(512)]
    public string Description { get; set; } = default!;

    
    public Guid MeetingId { get; set; }
    public Meeting? Meeting { get; set; }

}