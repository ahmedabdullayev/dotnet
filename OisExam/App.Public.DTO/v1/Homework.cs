namespace App.Public.DTO.v1;

public class Homework
{
    public Guid Id { get; set; }

    public string HW { get; set; } = default!;

    public int Grade { get; set; }

    public Guid EnrollmentId { get; set; }
    // public Enrollment? Enrollment { get; set; }
}