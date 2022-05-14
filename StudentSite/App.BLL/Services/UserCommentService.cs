using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserCommentService : BaseEntityService<App.BLL.DTO.UserComment, App.DAL.DTO.UserComment, IUserCommentRepository>,
    IUserCommentService
{
    public UserCommentService(IUserCommentRepository repository, IMapper<UserComment, DAL.DTO.UserComment> mapper) : base(repository, mapper)
    {
    }

    public UserComment AddWithUser(UserComment entity, Guid userId)
    {
        return Mapper.Map(Repository.AddWithUser(Mapper.Map(entity)!, userId))!;
    }
}