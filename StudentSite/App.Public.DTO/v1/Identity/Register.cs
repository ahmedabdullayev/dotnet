using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.Public.DTO.v1.Identity;

public class Register : IDomainEntityId
{
    [StringLength(maximumLength:128, MinimumLength = 5, ErrorMessage = "Wrong length on email")]
    public string Email { get; set; } = default!;
    [StringLength(maximumLength:128, MinimumLength = 1, ErrorMessage = "Wrong length on password")]
    public string Password { get; set; } = default!;

    [StringLength(maximumLength: 128, MinimumLength = 1, ErrorMessage = "Wrong length on FirstName")]
    public string Firstname { get; set; } = default!;

    [StringLength(maximumLength: 128, MinimumLength = 1, ErrorMessage = "Wrong length on LastName")]
    public string Lastname { get; set; } = default!;

    public Guid Id { get; set; }
}