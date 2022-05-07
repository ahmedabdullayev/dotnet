using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IQuizService : IEntityService<App.BLL.DTO.Quiz>, IQuizRepositoryCustom<App.BLL.DTO.Quiz>
{
    
}