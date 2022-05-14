using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserPostService : IEntityService<App.BLL.DTO.UserPost>, IUserPostRepositoryCustom<App.BLL.DTO.UserPost>

{
    
}