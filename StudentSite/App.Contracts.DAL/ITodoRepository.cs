using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ITodoRepository : IEntityRepository<App.DAL.DTO.Todo>, ITodoRepositoryCustom<Todo>
{
    
}

public interface ITodoRepositoryCustom<TEntity>
{
    
}