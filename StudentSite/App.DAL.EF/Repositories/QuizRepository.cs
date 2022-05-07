using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class QuizRepository: BaseEntityRepository<Quiz, App.Domain.Quiz, AppDbContext>, IQuizRepository
{
    public QuizRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Quiz,App.Domain.Quiz> mapper) 
        : base(dbContext, mapper)
    {
    }
    
    public override async Task<IEnumerable<Quiz>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(u => u.AppUser);

        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    // with ownership
    public async Task<IEnumerable<Quiz>> GetAllAsync(Guid userId,bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(u => u.AppUser).Where(m => m.AppUserId == userId);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}