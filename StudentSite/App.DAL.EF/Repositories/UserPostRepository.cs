using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserPostRepository: BaseEntityRepository<App.DAL.DTO.UserPost, App.Domain.UserPost, AppDbContext>, 
    IUserPostRepository
{
    public UserPostRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.UserPost,App.Domain.UserPost> mapper) 
        : base(dbContext, mapper)
    {
    }
}