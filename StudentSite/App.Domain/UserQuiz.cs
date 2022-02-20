using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserQuiz : DomainEntityMetaId
{
    public Guid QuizId { get; set; }
    public Quiz? Quiz { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public ICollection<UserChoice>? UserChoices { get; set; }
}