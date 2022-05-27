namespace App.DAL.EF.Repositories;

public class SubjectRepository : BaseRepository<App.Domain.Subject, AppDbContext>
{
    public SubjectRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}