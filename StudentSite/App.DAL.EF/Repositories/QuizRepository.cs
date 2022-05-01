using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class QuizRepository: BaseEntityRepository<Quiz, AppDbContext>, IQuizRepository
{
    public QuizRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    public override async Task<IEnumerable<Quiz>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(u => u.AppUser);

        return await query.ToListAsync();
    }
    // with ownership
    public async Task<IEnumerable<Quiz>> GetAllAsync(Guid userId,bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(u => u.AppUser).Where(m => m.AppUserId == userId);
        return await query.ToListAsync();
    }
}