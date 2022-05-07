using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class TodoRepository: BaseEntityRepository<App.DAL.DTO.Todo, App.Domain.Todo, AppDbContext>, ITodoRepository
{
    public TodoRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Todo,App.Domain.Todo> mapper) 
        : base(dbContext, mapper)
    {
    }
}