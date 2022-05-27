namespace App.DAL.EF.Repositories;

public class AppRoleRepository: BaseRepository<App.Domain.Identity.AppRole, AppDbContext>
{
    public AppRoleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}