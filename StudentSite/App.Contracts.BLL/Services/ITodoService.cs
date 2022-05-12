using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ITodoService : IEntityService<App.BLL.DTO.Todo>, ITodoRepositoryCustom<App.BLL.DTO.Todo>
{
    
}