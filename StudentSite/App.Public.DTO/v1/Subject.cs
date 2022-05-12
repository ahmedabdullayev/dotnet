using Base.Domain;

namespace App.Public.DTO.v1;

public class Subject : DomainEntityId
{
    // LangStr
    public string Name { get; set; } = default!;
    
    public string Description { get; set; } = default!;

    public ICollection<Quiz>? Quizzes { get; set; }
}