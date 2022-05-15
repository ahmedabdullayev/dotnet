using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserQuizRepository : IEntityRepository<App.DAL.DTO.UserQuiz>, IUserQuizRepositoryCustom<UserQuiz>
{
    
}

public interface IUserQuizRepositoryCustom<TEntity>
{
    TEntity AddWithUser(TEntity entity, Guid userId);
    
}