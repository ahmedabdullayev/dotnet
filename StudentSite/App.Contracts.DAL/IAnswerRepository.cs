using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAnswerRepository : IEntityRepository<App.DAL.DTO.Answer>, IAnswerRepositoryCustom<Answer>
{
    
}
public interface IAnswerRepositoryCustom<TEntity> 
{

}