using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ISubjectRepository : IEntityRepository<App.DAL.DTO.Subject>, ISubjectRepositoryCustom<Subject>
{
    
}

public interface ISubjectRepositoryCustom<TEntity>
{
    
}