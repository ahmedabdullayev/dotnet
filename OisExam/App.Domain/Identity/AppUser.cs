using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    [StringLength(128)]
    public string Firstname { get; set; } = default!;

    [StringLength(128)] public string Lastname { get; set; } = default!;

    public ICollection<Enrollment>? Enrollments { get; set; }
}
