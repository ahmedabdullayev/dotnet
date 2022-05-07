using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class AnswerService : BaseEntityService<App.BLL.DTO.Answer, App.DAL.DTO.Answer, IAnswerRepository>, 
        IAnswerService
{
        public AnswerService(IAnswerRepository repository, IMapper<Answer, DAL.DTO.Answer> mapper) : base(repository, mapper)
        {
        }
}