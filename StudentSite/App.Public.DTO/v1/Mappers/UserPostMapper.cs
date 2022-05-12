using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserPostMapper: BaseMapper<Public.DTO.v1.UserPost, App.BLL.DTO.UserPost>
{
    public UserPostMapper(IMapper mapper) : base(mapper)
    {
    }
}