using Base.Contracts.Domain;

namespace App.Domain;

public class Homework: IDomainEntityId
{
    public Guid Id { get; set; }

    public string HW { get; set; } = default!;

    public int Grade { get; set; } = default!;

    public Guid EnrollmentId { get; set; }
    public Enrollment? Enrollment { get; set; }
}