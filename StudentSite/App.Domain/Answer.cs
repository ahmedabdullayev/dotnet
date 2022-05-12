using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Answer : DomainEntityId
{
    
    [Column(TypeName = "jsonb")] // convert to json and save as string, and when we get it deserialize it and return object

    public LangStr AnswerText { get; set; } = new();
    
    public bool IsCorrect { get; set; }
    
    public Guid QuestionId { get; set; }
    public Question? Question { get; set; }
    
    public ICollection<UserChoice>? UserChoices { get; set; }
}