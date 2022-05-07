using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserQuizRepository: BaseEntityRepository<App.DAL.DTO.UserQuiz, App.Domain.UserQuiz, AppDbContext>, 
    IUserQuizRepository
{
    public UserQuizRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.UserQuiz,App.Domain.UserQuiz> mapper) 
        : base(dbContext, mapper)
    {
    }
}