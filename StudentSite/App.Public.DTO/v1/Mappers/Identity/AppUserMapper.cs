using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers.Identity;

public class AppUserMapper : BaseMapper<Public.DTO.v1.Identity.AppUser, App.BLL.DTO.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}