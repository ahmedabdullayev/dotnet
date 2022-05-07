using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserPostMapper: BaseMapper<App.DAL.DTO.UserPost, App.Domain.UserPost>
{
    public UserPostMapper(IMapper mapper) : base(mapper)
    {
    }
}