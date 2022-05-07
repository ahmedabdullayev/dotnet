using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DAL.DTO;

public class Answer : DomainEntityId
{
    [MaxLength(255)]
    public string AnswerText { get; set; } = default!;
    
    public bool IsCorrect { get; set; }
    
    public Guid QuestionId { get; set; }
    public Question? Question { get; set; }
    
    public ICollection<UserChoice>? UserChoices { get; set; }
}