using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class UserQuiz : DomainEntityId
{
    public Guid QuizId { get; set; }
    // public Quiz? Quiz { get; set; }

    public Guid AppUserId { get; set; }
    // public AppUser? AppUser { get; set; }
    
    public ICollection<UserChoice>? UserChoices { get; set; }
}