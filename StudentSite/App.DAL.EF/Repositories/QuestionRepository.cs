using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class QuestionRepository: BaseEntityRepository<App.DAL.DTO.Question, App.Domain.Question, AppDbContext>, IQuestionRepository
{
public QuestionRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Question,App.Domain.Question> mapper) 
    : base(dbContext, mapper)
{
}

public override async Task<IEnumerable<Question>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        return (await query.Include(s => s.Answers).ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    public override Question Update(Question entity)
    {
        var realEntity = RepoDbSet.FindAsync(entity.Id).Result;

        realEntity!.QuestionText.SetTranslation(entity.QuestionText);
        return Mapper.Map(RepoDbSet.Update(realEntity).Entity)!;
    }
    public override async Task<Question?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Include(q => q.Answers);
        return  Mapper.Map(await query.FirstOrDefaultAsync(m => m.Id.Equals(id)));
    }
}