using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.DAL.DTO;

public class Subject : DomainEntityId
{
    // LangStr
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public ICollection<Quiz>? Quizzes { get; set; }
}