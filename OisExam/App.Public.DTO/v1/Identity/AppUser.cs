using System.ComponentModel.DataAnnotations;

namespace App.Public.DTO.v1.Identity;

public class AppUser
{
    public Guid Id { get; set; }
    [MaxLength(64)] public string FirstName { get; set; } = default!;
        
    [MaxLength(64)] public string LastName { get; set; } = default!;
        
    [MaxLength(256)] public string Email { get; set; } = default!;
}
