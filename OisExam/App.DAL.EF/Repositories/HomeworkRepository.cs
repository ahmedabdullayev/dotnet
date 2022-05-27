using App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class HomeworkRepository : BaseRepository<App.Domain.Homework, AppDbContext>
{
    public HomeworkRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public void UpdateGrade(Guid guid, Homework entity)
    {
        var homework = RepoDbSet.FirstAsync(x => x.Id == guid).Result;

        homework.Grade = entity.Grade;
    }
}