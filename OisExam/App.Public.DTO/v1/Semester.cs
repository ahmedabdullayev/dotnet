namespace App.Public.DTO.v1;

public class Semester
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    // public ICollection<Enrollment>? Enrollments { get; set; }
}