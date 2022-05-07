using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class AnswerRepository: BaseEntityRepository<App.DAL.DTO.Answer, App.Domain.Answer, AppDbContext>, IAnswerRepository
{
    public AnswerRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Answer,App.Domain.Answer> mapper) 
        : base(dbContext, mapper)
    {
    }
}