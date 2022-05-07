using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class UserQuiz : DomainEntityId
{
    public Guid QuizId { get; set; }
    public Quiz? Quiz { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public ICollection<UserChoice>? UserChoices { get; set; }
}