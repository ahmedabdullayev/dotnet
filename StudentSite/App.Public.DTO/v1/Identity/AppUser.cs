using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1.Identity;

public class AppUser: DomainEntityId
{
    [StringLength(128)] public string FirstName { get; set; } = default!;
    [StringLength(128)] public string LastName { get; set; } = default!;
    [MaxLength(256)] public string Email { get; set; } = default!;

}