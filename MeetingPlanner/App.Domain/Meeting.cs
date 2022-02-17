using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Meeting: DomainEntityMetaId
{
    [MaxLength(128)]
    public string Title { get; set; } = default!;

    [MaxLength(4096)]
    public string Description { get; set; } = default!;
    
    [MaxLength(512)]
    public string Location { get; set; } = default!;

    public bool UseNotIdeal { get; set; }

    public bool LimitVoteNumber { get; set; }

    public bool LimitToSingleVote { get; set; } = true;

    public bool IsDataHidden { get; set; }

    public DateTime AvailableFrom { get; set; }
    public DateTime DeadLine { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public ICollection<MeetingOption>? MeetingOptions { get; set; }

}