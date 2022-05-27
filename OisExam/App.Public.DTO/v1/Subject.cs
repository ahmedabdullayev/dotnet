namespace App.Public.DTO.v1;

public class Subject
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
    
    // public ICollection<Enrollment>? Enrollments { get; set; }
}