using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TodoRepository: BaseEntityRepository<App.DAL.DTO.Todo, App.Domain.Todo, AppDbContext>, ITodoRepository
{
    public TodoRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Todo,App.Domain.Todo> mapper) 
        : base(dbContext, mapper)
    {
    }
    
    

    // with ownership 
    public async Task<IEnumerable<Todo>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(u => u.AppUser).Where(m => m.AppUserId == userId);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public override Todo Add(Todo entity)
    {
        
        return base.Add(entity);
    }

    public Todo AddWithUser(Todo entity, Guid userQuizId)
    {
        entity.AppUserId = userQuizId;
        
        return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
    }

    public Todo UpdateWithUser(Todo entity, Guid userQuizId)
    {
        entity.AppUserId = userQuizId;
        return Mapper.Map(RepoDbSet.Update(Mapper.Map(entity)!).Entity)!;
    }
        //for sesurity
    public async Task<Todo?> FirstWithUser(Guid id, Guid userTodoId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(u => u.AppUser);
        var result = Mapper.Map(await query.FirstOrDefaultAsync(u => u.Id == id && u.AppUserId == userTodoId));
        return result;
    }
}