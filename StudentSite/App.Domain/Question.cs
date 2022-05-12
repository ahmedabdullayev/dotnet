using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Question : DomainEntityMetaId // TODO UPDATE DB!
{
    [Column(TypeName = "jsonb")] // convert to json and save as string, and when we get it deserialize it and return object
    public LangStr QuestionText { get; set; } = new();

    public Guid QuizId { get; set; }
    public Quiz? Quiz { get; set; }

    public ICollection<Answer>? Answers { get; set; }
    public ICollection<UserChoice>? UserChoices { get; set; }
}