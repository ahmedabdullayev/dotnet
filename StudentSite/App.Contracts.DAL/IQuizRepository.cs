using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IQuizRepository : IEntityRepository<App.DAL.DTO.Quiz>, IQuizRepositoryCustom<App.DAL.DTO.Quiz>
{
}

public interface IQuizRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
}