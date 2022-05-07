using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ITopicRepository : IEntityRepository<App.DAL.DTO.Topic>, ITopicRepositoryCustom<Topic>
{
    
}

public interface ITopicRepositoryCustom<TEntity>
{
    
}