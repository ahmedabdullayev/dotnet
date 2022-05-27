using Base.Contracts.Domain;

namespace App.Domain;

public class Semester : IDomainEntityId
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public ICollection<Enrollment>? Enrollments { get; set; }
}