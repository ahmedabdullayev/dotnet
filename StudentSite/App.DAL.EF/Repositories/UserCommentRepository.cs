using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserCommentRepository: BaseEntityRepository<App.DAL.DTO.UserComment, App.Domain.UserComment, AppDbContext>, 
    IUserCommentRepository
{
    public UserCommentRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.UserComment,App.Domain.UserComment> mapper) 
        : base(dbContext, mapper)
    {
    }
}