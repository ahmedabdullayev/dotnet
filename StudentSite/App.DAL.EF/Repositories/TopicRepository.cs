using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class TopicRepository: BaseEntityRepository<App.DAL.DTO.Topic, App.Domain.Topic, AppDbContext>, ITopicRepository
{
    public TopicRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Topic,App.Domain.Topic> mapper) 
        : base(dbContext, mapper)
    {
    }
}