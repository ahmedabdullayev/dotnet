using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUserRepository: IEntityRepository<App.DAL.DTO.Identity.AppUser>, IAnswerRepositoryCustom<App.DAL.DTO.Identity.AppUser>
{
    
}
public interface IAppUserRepositoryCustom<TEntity> 
{

}