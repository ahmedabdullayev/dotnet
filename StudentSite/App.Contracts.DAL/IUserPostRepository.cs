using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserPostRepository : IEntityRepository<App.DAL.DTO.UserPost>, IUserPostRepositoryCustom<UserPost>
{
    
}

public interface IUserPostRepositoryCustom<TEntity>
{
    TEntity AddWithUser(TEntity entity, Guid userId);

}