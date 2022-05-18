using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class AppUserRepository: BaseEntityRepository<App.DAL.DTO.Identity.AppUser, App.Domain.Identity.AppUser, AppDbContext>, IAppUserRepository
{
    public AppUserRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Identity.AppUser,App.Domain.Identity.AppUser> mapper) 
        : base(dbContext, mapper)
    {
    }
    
}