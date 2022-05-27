
using App.Public.DTO.v1.Identity;

namespace App.Public.DTO.v1;

public class Enrollment
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