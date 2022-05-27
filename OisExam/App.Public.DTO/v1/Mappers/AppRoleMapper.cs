namespace App.Public.DTO.v1.Mappers;

public class AppRoleMapper
{
    public App.Public.DTO.v1.Identity.AppRole MapToPublic(App.Domain.Identity.AppRole entity)
    {
        return new App.Public.DTO.v1.Identity.AppRole()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
        
    public App.Domain.Identity.AppRole MapFromPublic(App.Public.DTO.v1.Identity.AppRole entity)
    {
        return new App.Domain.Identity.AppRole()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}