using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserQuizService : IEntityService<App.BLL.DTO.UserQuiz>, IUserQuizRepositoryCustom<App.BLL.DTO.UserQuiz>
{
    
}