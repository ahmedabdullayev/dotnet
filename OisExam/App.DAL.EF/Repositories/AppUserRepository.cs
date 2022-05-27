namespace App.DAL.EF.Repositories;

public class AppUserRepository: BaseRepository<App.Domain.Identity.AppUser, AppDbContext>
{
    public AppUserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}