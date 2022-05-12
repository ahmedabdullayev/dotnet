using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ITodoRepository : IEntityRepository<App.DAL.DTO.Todo>, ITodoRepositoryCustom<App.DAL.DTO.Todo>
{
    
}

public interface ITodoRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);

}