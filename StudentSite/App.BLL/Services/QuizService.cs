using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class QuizService : BaseEntityService<App.BLL.DTO.Quiz, App.DAL.DTO.Quiz, IQuizRepository>,
    IQuizService
{
    public QuizService(IQuizRepository repository, IMapper<Quiz, DAL.DTO.Quiz> mapper) : base(repository, mapper)
    {
    }
    
    public async Task<IEnumerable<Quiz>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        return (await Repository.GetAllAsync(userId, noTracking)).Select(x => Mapper.Map(x)!);
    }
}