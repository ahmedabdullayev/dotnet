namespace App.Public.DTO.v1.Mappers;

public class EnrollmentMapper
{
    private readonly AppUserMapper _appUserMapper;
    private readonly SubjectMapper _subjectMapper;
    private readonly SemesterMapper _semesterMapper;
    public EnrollmentMapper()
    {
        _appUserMapper = new AppUserMapper();
        _subjectMapper = new SubjectMapper();
        _semesterMapper = new SemesterMapper();
    }

    public App.Public.DTO.v1.Enrollment MapToPublic(App.Domain.Enrollment entity)
    {
        List<App.Public.DTO.v1.Homework> homeworks = new List<Homework>();
        if (entity.Homeworks != null)
        {
            homeworks = (entity.Homeworks.ToList()).Select(e => new App.Public.DTO.v1.Homework()
            {
                Id = e.Id,
                HW = e.HW,
                Grade = e.Grade,
                EnrollmentId = e.EnrollmentId
            }).ToList();
        }

        return new App.Public.DTO.v1.Enrollment()
        {
            Id = entity.Id,
            IsTeacher = entity.IsTeacher,
            IsAccepted = entity.IsAccepted,
            AppUserId = entity.AppUserId,
            AppUser = _appUserMapper.MapToPublic(entity.AppUser!),
            SubjectId = entity.SubjectId,
            Subject = _subjectMapper.MapToPublic(entity.Subject!),
            SemesterId = entity.SemesterId,
            Semester = _semesterMapper.MapToPublic(entity.Semester!),
            Homeworks = homeworks,
        };
        
    }
    
    public App.Public.DTO.v1.Enrollment MapToPublicAdd(App.Domain.Enrollment entity)
    {
        List<App.Public.DTO.v1.Homework> homeworks = new List<Homework>();
        if (entity.Homeworks != null)
        {
            homeworks = (entity.Homeworks.ToList()).Select(e => new App.Public.DTO.v1.Homework()
            {
                Id = e.Id,
                HW = e.HW,
                Grade = e.Grade,
                EnrollmentId = e.EnrollmentId
            }).ToList();
        }

        return new App.Public.DTO.v1.Enrollment()
        {
            Id = entity.Id,
            IsTeacher = entity.IsTeacher,
            IsAccepted = entity.IsAccepted,
            AppUserId = entity.AppUserId,
            // AppUser = _appUserMapper.MapToPublic(entity.AppUser!),
            SubjectId = entity.SubjectId,
            SemesterId = entity.SemesterId,
            Homeworks = homeworks,
        };
        
    }

    public App.Domain.Enrollment MapFromPublic(App.Public.DTO.v1.Enrollment entity)
    {
        List<App.Domain.Homework> enrollments = new();
        if (entity.Homeworks != null)
        {
            enrollments = (entity.Homeworks.ToList()).Select(e => new App.Domain.Homework()
            {
                Id = e.Id,
                HW = e.HW,
                Grade = e.Grade,
                EnrollmentId = e.EnrollmentId
            }).ToList();
        }
            
        return new App.Domain.Enrollment()
        {
            Id = entity.Id,
            IsTeacher = entity.IsTeacher,
            IsAccepted = entity.IsAccepted,
            AppUserId = entity.AppUserId,
            AppUser = _appUserMapper.MapFromPublic(entity.AppUser!),
            SubjectId = entity.SubjectId,
            SemesterId = entity.SemesterId,
            Homeworks = enrollments,
        };
    }
    
    public App.Domain.Enrollment MapFromPublicAccept(App.Public.DTO.v1.Enrollment entity)
    {
        List<App.Domain.Homework> enrollments = new();
        if (entity.Homeworks != null)
        {
            enrollments = (entity.Homeworks.ToList()).Select(e => new App.Domain.Homework()
            {
                Id = e.Id,
                HW = e.HW,
                Grade = e.Grade,
                EnrollmentId = e.EnrollmentId
            }).ToList();
        }
            
        return new App.Domain.Enrollment()
        {
            Id = entity.Id,
            IsTeacher = false,
            IsAccepted = entity.IsAccepted,
            AppUserId = entity.AppUserId,
            // AppUser = _appUserMapper.MapFromPublic(entity.AppUser!),
            SubjectId = entity.SubjectId,
            SemesterId = entity.SemesterId,
            Homeworks = enrollments,
        };
    }
    
    public App.Domain.Enrollment MapFromPublicAcceptNoUser(App.Public.DTO.v1.Enrollment entity)
    {
        List<App.Domain.Homework> enrollments = new();
        if (entity.Homeworks != null)
        {
            enrollments = (entity.Homeworks.ToList()).Select(e => new App.Domain.Homework()
            {
                Id = e.Id,
                HW = e.HW,
                Grade = e.Grade,
                EnrollmentId = e.EnrollmentId
            }).ToList();
        }
            
        return new App.Domain.Enrollment()
        {
            Id = entity.Id,
            IsTeacher = entity.IsTeacher,
            IsAccepted = entity.IsAccepted,
            AppUserId = entity.AppUserId,
            // AppUser = _appUserMapper.MapFromPublic(entity.AppUser!),
            SubjectId = entity.SubjectId,
            SemesterId = entity.SemesterId,
            Homeworks = enrollments,
        };
    }

}