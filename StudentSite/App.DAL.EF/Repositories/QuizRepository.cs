using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class QuizRepository: BaseEntityRepository<Quiz, App.Domain.Quiz, AppDbContext>, IQuizRepository
{
    public QuizRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Quiz,App.Domain.Quiz> mapper) 
        : base(dbContext, mapper)
    {
    }
    
    public override async Task<IEnumerable<Quiz>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    public override Quiz Add(Quiz entity)
    {
        var ent = new App.Domain.Quiz();
        // ent.AppUserId = entity.AppUserId;
        ent.SubjectId = entity.SubjectId;
        ent.IsReady = entity.IsReady;
        ent.Name.SetTranslation(entity.Name);
        ent.Description.SetTranslation(entity.Description);
        
        return Mapper.Map(RepoDbSet.Add(ent).Entity)!;
    }

    public override Quiz Update(Quiz entity)
    {
        var realEntity = RepoDbSet.FindAsync(entity.Id).Result;
        
        realEntity!.Name.SetTranslation(entity.Name);
        realEntity.Description.SetTranslation(entity.Description);
        
        return Mapper.Map(RepoDbSet.Update(realEntity).Entity)!;
    }
    // with ownership TODO REMOVE THIS
    public async Task<IEnumerable<Quiz>> GetAllAsync(Guid userId,bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.Include(m => m.Questions).ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public async Task<IEnumerable<Quiz>> GetAllAsyncBySubject(Guid subjectId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(s => s.Subject).Where(m => m.SubjectId == subjectId);
        return (await query.Include(m => m.Subject).ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    public override async Task<Quiz?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(q => q.Questions);
        return  Mapper.Map(await query.FirstOrDefaultAsync(m => m.Id.Equals(id)));
    }

}