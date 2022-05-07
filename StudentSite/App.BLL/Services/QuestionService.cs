using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class QuestionService : BaseEntityService<App.BLL.DTO.Question, App.DAL.DTO.Question, IQuestionRepository>,
    IQuestionService
{
    public QuestionService(IQuestionRepository repository, IMapper<Question, DAL.DTO.Question> mapper) : base(repository, mapper)
    {
    }
}