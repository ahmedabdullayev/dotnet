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

    public override Todo Add(Todo entity)
    {
        var ent = new App.Domain.Todo();
        ent.AppUserId = entity.AppUserId;
        
        return Mapper.Map(RepoDbSet.Add(ent).Entity)!;
    }

    public override Todo Update(Todo entity)
    {
        var ent = new App.Domain.Todo
        {
            Id = entity.Id,
            AppUserId = entity.AppUserId
        };
        return Mapper.Map(RepoDbSet.Update(ent).Entity)!;
    }
    // with ownership 
    public async Task<IEnumerable<Todo>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(u => u.AppUser).Where(m => m.AppUserId == userId);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}