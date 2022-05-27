namespace App.Public.DTO.v1.Mappers;

public class SemesterMapper
{
    public SemesterMapper()
    {
        
    }

    public App.Public.DTO.v1.Semester MapToPublic(App.Domain.Semester entity)
    {
        List<App.Public.DTO.v1.Enrollment> enrollments = new List<Enrollment>();
        if (entity.Enrollments != null)
        {
            enrollments = (entity.Enrollments.ToList()).Select(e => new App.Public.DTO.v1.Enrollment()
            {
                Id = e.Id,
                IsTeacher = e.IsTeacher,
                IsAccepted = e.IsAccepted,
                AppUserId = e.AppUserId,
                SubjectId = e.SubjectId,
                SemesterId = e.SemesterId
            }).ToList();
        }

        return new App.Public.DTO.v1.Semester()
        {
            Id = entity.Id,
            Name = entity.Name,
            // Enrollments = enrollments
        };
    }

    public App.Domain.Semester MapFromPublic(App.Public.DTO.v1.Semester entity)
    {
        List<App.Domain.Enrollment> enrollments = new();
        // if (entity.Enrollments != null)
        // {
        //     enrollments = (entity.Enrollments.ToList()).Select(e => new App.Domain.Enrollment()
        //     {
        //         Id = e.Id,
        //         IsTeacher = e.IsTeacher,
        //         IsAccepted = e.IsAccepted,
        //         AppUserId = e.AppUserId,
        //         SubjectId = e.SubjectId,
        //         SemesterId = e.SemesterId
        //     }).ToList();
        // }
            
        return new App.Domain.Semester()
        {
            Id = entity.Id,
            Name = entity.Name,
            // Enrollments = enrollments
        };
    }

}