using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IQuizRepository : IEntityRepository<Quiz>
{
    Task<IEnumerable<Quiz>> GetAllAsync(Guid userId, bool noTracking = true);
}