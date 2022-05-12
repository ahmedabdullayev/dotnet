using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserChoiceService : IEntityService<App.BLL.DTO.UserChoice>, IUserChoiceRepositoryCustom<App.BLL.DTO.UserChoice>
{
    
}