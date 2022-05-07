using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class QuestionRepository: BaseEntityRepository<App.DAL.DTO.Question, App.Domain.Question, AppDbContext>, IQuestionRepository
{
public QuestionRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Question,App.Domain.Question> mapper) 
    : base(dbContext, mapper)
{
}
}