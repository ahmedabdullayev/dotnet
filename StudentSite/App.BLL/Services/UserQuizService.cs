using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserQuizService : BaseEntityService<App.BLL.DTO.UserQuiz, App.DAL.DTO.UserQuiz, IUserQuizRepository>,
    IUserQuizService
{
    public UserQuizService(IUserQuizRepository repository, IMapper<UserQuiz, DAL.DTO.UserQuiz> mapper) : base(repository, mapper)
    {
    }
}