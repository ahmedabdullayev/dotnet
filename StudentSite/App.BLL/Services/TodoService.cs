using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class TodoService : BaseEntityService<App.BLL.DTO.Todo, App.DAL.DTO.Todo, ITodoRepository>,
    ITodoService
{
    public TodoService(ITodoRepository repository, IMapper<BLL.DTO.Todo, DAL.DTO.Todo> mapper) : base(repository, mapper)
    {
    }
    
    public async Task<IEnumerable<Todo>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        
        return (await Repository.GetAllAsync(userId, noTracking)).Select(x => Mapper.Map(x)!);
    }

    public Todo AddWithUser(Todo entity, Guid userQuizId)
    {
        return Mapper.Map(Repository.AddWithUser(Mapper.Map(entity)!, userQuizId))!;
    }

    public Todo UpdateWithUser(Todo entity, Guid userQuizId)
    {
        return Mapper.Map(Repository.UpdateWithUser(Mapper.Map(entity)!, userQuizId))!;
    }

    public async Task<Todo?> FirstWithUser(Guid id, Guid userTodoId, bool noTracking = true)
    {
        return Mapper.Map(await Repository.FirstWithUser(id, userTodoId)!);
    }
}