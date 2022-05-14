using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserCommentRepository : IEntityRepository<App.DAL.DTO.UserComment>, IUserCommentRepositoryCustom<UserComment>
{
    
}

public interface IUserCommentRepositoryCustom<TEntity>
{
    TEntity AddWithUser(TEntity entity, Guid userId);
}