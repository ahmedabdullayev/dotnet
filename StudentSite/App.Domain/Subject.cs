using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Subject : DomainEntityId
{
    [MaxLength(50)]
    public string Name { get; set; } = default!;
    [MaxLength(255)]
    public string Description { get; set; } = default!;

    public ICollection<Quiz>? Quizzes { get; set; }
}