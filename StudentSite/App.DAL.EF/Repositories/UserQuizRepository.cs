using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserQuizRepository: BaseEntityRepository<App.DAL.DTO.UserQuiz, App.Domain.UserQuiz, AppDbContext>, 
    IUserQuizRepository
{
    public UserQuizRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.UserQuiz,App.Domain.UserQuiz> mapper) 
        : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<UserQuiz>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        return (await query.Include(s => s.UserChoices).ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public override async Task<UserQuiz?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        var domQuery = await query
            .Include(q => q.UserChoices)!
            .ThenInclude(a => a.Answer)
            .Include(m => m.UserChoices)!
            .ThenInclude(t => t.Question)
            .FirstOrDefaultAsync(m => m.Id.Equals(id));
        foreach (var ddd in domQuery!.UserChoices!)
        {
            Console.WriteLine(ddd.Answer!.IsCorrect);
        }
        return  Mapper.Map(domQuery);
    }

    public UserQuiz AddWithUser(UserQuiz entity, Guid userId)
    {
        entity.AppUserId = userId;
        
        return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
    }
}