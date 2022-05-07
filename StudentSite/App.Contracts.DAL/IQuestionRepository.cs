using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IQuestionRepository : IEntityRepository<App.DAL.DTO.Question>, IQuestionRepositoryCustom<Question>
{
    
}

public interface IQuestionRepositoryCustom<TEntity>
{
    
}