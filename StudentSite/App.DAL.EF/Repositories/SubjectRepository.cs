using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class SubjectRepository : BaseEntityRepository<App.DAL.DTO.Subject, App.Domain.Subject, AppDbContext>, ISubjectRepository
{
    public SubjectRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Subject,App.Domain.Subject> mapper) 
        : base(dbContext, mapper)
    {
    }

    // public override Subject Add(Subject entity)
    // {
    //     var subj = new App.Domain.Subject();
    //     subj.Name.SetTranslation(entity.Name);
    //     subj.Description.SetTranslation(entity.Description);
    //
    //     return Mapper.Map(RepoDbSet.Add(subj).Entity)!;
    // }


    public override Subject Update(Subject entity)
    {
        var realEntity = RepoDbSet.FindAsync(entity.Id).Result;
        
        realEntity!.Name.SetTranslation(entity.Name);
        realEntity.Description.SetTranslation(entity.Description);
        
        return Mapper.Map(RepoDbSet.Update(realEntity).Entity)!;
    }

    public override async Task<IEnumerable<Subject>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        return (await query.Include(s => s.Quizzes).ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public override async Task<Subject?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(q => q.Quizzes);
        return  Mapper.Map(await query.FirstOrDefaultAsync(m => m.Id.Equals(id)));
    }
}