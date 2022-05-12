using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserCommentMapper: BaseMapper<Public.DTO.v1.UserComment, App.BLL.DTO.UserComment>
{
    public UserCommentMapper(IMapper mapper) : base(mapper)
    {
    }
}