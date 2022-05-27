namespace App.Public.DTO.v1.Mappers;

public class SubjectMapper
{

    public App.Public.DTO.v1.Subject MapToPublic(App.Domain.Subject entity)
    {
        List<App.Public.DTO.v1.Enrollment> enrollments = new List<Enrollment>();
        if (entity.Enrollments != null)
        {
            enrollments = (entity.Enrollments.ToList()).Select(e => new App.Public.DTO.v1.Enrollment()
            {
                Id = e.Id,
                IsTeacher = e.IsTeacher,
                AppUserId = e.AppUserId,
                SubjectId = e.SubjectId,
                SemesterId = e.SemesterId
            }).ToList();
        }

        return new App.Public.DTO.v1.Subject()
        {
            Id = entity.Id,
            Name = entity.Name,
            // Enrollments = enrollments
        };
    }

    public App.Domain.Subject MapFromPublic(App.Public.DTO.v1.Subject entity)
    {
        List<App.Domain.Enrollment> enrollments = new();
        // if (entity.Enrollments != null)
        // {
        //     enrollments = (entity.Enrollments.ToList()).Select(e => new App.Domain.Enrollment()
        //     {
        //         Id = e.Id,
        //         IsTeacher = e.IsTeacher,
        //         AppUserId = e.AppUserId,
        //         SubjectId = e.SubjectId,
        //         SemesterId = e.SemesterId
        //     }).ToList();
        // }
            
        return new App.Domain.Subject()
        {
            Id = entity.Id,
            Name = entity.Name,
            // Enrollments = enrollments
        };
    }

}