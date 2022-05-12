using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TopicRepository: BaseEntityRepository<App.DAL.DTO.Topic, App.Domain.Topic, AppDbContext>, ITopicRepository
{
    public TopicRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Topic,App.Domain.Topic> mapper) 
        : base(dbContext, mapper)
    {
    }


    public override async Task<IEnumerable<Topic>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        return (await query.Include(s => s.UserPosts).ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}