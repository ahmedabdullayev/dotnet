using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class UserChoice : DomainEntityId
{
    public Guid QuizId { get; set; }
    //public Quiz? Quiz { get; set; }

    public Guid QuestionId { get; set; }
    public Question? Question { get; set; }

    public Guid AnswerId { get; set; }
    public Answer? Answer { get; set; }

    public Guid UserQuizId { get; set; }
   // public UserQuiz? UserQuiz { get; set; }
    
    public Guid AppUserId { get; set; }
    //public AppUser? AppUser { get; set; }
}