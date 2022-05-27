using App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class SemesterRepository : BaseRepository<App.Domain.Semester, AppDbContext>
{
    public SemesterRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Semester>> GetAllAsync(bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        query = query.Include(m => m.Enrollments);

        return await query.ToListAsync();
    }
}