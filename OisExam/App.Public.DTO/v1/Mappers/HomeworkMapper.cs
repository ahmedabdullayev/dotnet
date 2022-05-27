
namespace App.Public.DTO.v1.Mappers;

public class HomeworkMapper
{

    public HomeworkMapper()
    {
    }

    public App.Public.DTO.v1.Homework MapToPublic(App.Domain.Homework entity)
    {
        return new App.Public.DTO.v1.Homework()
        {
            Id = entity.Id,
            HW = entity.HW,
            Grade = entity.Grade,
            EnrollmentId = entity.EnrollmentId
        };
            
    }
        
    public App.Domain.Homework MapFromPublic(App.Public.DTO.v1.Homework entity)
    {
        return new App.Domain.Homework()
        {
            Id = entity.Id,
            HW = entity.HW,
            Grade = entity.Grade,
            EnrollmentId = entity.EnrollmentId
        };
           
    }
}