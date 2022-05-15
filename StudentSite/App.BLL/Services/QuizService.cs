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
    

    public async Task<IEnumerable<Quiz>> GetAllAsyncBySubject(Guid subjectId, bool noTracking = true)
    {
        return (await Repository.GetAllAsyncBySubject(subjectId, noTracking)).Select(x => Mapper.Map(x)!);

    }

    public Task<Quiz> FirstOrDefaultWithQuizzes(Guid subjectId, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}