namespace App.Public.DTO.v1.Mappers;

public class AppUserMapper
{
    public App.Public.DTO.v1.Identity.AppUser MapToPublic(App.Domain.Identity.AppUser entity)
    {
        return new App.Public.DTO.v1.Identity.AppUser()
        {
            Id = entity.Id,
            FirstName = entity.Firstname,
            LastName = entity.Lastname,
            Email = entity.Email
        };
    }
        
    public App.Domain.Identity.AppUser MapFromPublic(App.Public.DTO.v1.Identity.AppUser entity)
    {
        return new App.Domain.Identity.AppUser()
        {
            Id = entity.Id,
            Firstname = entity.FirstName,
            Lastname = entity.LastName,
            Email = entity.Email
        };
    }
}