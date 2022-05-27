using System.Collections;
using App.Domain.Identity;
using Base.Contracts.Domain;

namespace App.Domain;

public class Enrollment: IDomainEntityId
{
    public Guid Id { get; set; }

    public bool IsTeacher { get; set; }
    
    public bool IsAccepted { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    public Guid SemesterId { get; set; }
    public Semester? Semester { get; set; }

    public ICollection<Homework>? Homeworks { get; set; }
}