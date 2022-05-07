using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserCommentMapper: BaseMapper<App.DAL.DTO.UserComment, App.Domain.UserComment>
{
    public UserCommentMapper(IMapper mapper) : base(mapper)
    {
    }
}