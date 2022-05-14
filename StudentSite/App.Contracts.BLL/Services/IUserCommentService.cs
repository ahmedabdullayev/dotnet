using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserCommentService : IEntityService<App.BLL.DTO.UserComment>, IUserCommentRepositoryCustom<App.BLL.DTO.UserComment>
{
    
}