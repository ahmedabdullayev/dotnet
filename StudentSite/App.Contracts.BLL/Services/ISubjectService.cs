using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ISubjectService : IEntityService<App.BLL.DTO.Subject>, ISubjectRepositoryCustom<App.BLL.DTO.Subject>
{
    
}