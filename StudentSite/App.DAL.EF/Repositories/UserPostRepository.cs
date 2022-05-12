using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserPostRepository: BaseEntityRepository<App.DAL.DTO.UserPost, App.Domain.UserPost, AppDbContext>, 
    IUserPostRepository
{
    public UserPostRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.UserPost,App.Domain.UserPost> mapper) 
        : base(dbContext, mapper)
    {
    }
    
    public override async Task<IEnumerable<UserPost>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        return (await query.Include(s => s.UserComments).ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}