using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserPostMapper: BaseMapper<App.BLL.DTO.UserPost, App.DAL.DTO.UserPost>
{
    public UserPostMapper(IMapper mapper) : base(mapper)
    {
    }
}