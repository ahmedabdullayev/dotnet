using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserChoiceRepository: IEntityRepository<App.DAL.DTO.UserChoice>, IUserChoiceRepositoryCustom<UserChoice>
{
    
}

public interface IUserChoiceRepositoryCustom<TEntity>
{

    Task<TEntity> GetWithLogic(TEntity entity, Guid userId);
    TEntity AddWithUser(TEntity entity, Guid userId);
}