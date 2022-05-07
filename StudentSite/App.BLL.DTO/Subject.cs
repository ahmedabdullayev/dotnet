using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.BLL.DTO;

public class Subject : DomainEntityId
{
    [Column(TypeName = "jsonb")] // convert to json and save as string, and when we get it deserialize it and return object
    public LangStr Name { get; set; } = new();
    
    [Column(TypeName = "jsonb")] // convert to json and save as string, and when we get it deserialize it and return object
    public LangStr Description { get; set; } = new();

    public ICollection<Quiz>? Quizzes { get; set; }
}