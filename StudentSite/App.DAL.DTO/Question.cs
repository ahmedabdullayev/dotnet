using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DAL.DTO;

public class Question : DomainEntityId // TODO UPDATE DB!
{
    [MaxLength(200)]
    public string QuestionText { get; set; } = default!;

    public Guid QuizId { get; set; }
    public Quiz? Quiz { get; set; }

    public ICollection<Answer>? Answers { get; set; }
    // public ICollection<UserChoice>? UserChoices { get; set; }
}