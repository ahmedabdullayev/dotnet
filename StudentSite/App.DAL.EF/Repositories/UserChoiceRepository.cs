using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserChoiceRepository: BaseEntityRepository<App.DAL.DTO.UserChoice, App.Domain.UserChoice, AppDbContext>, 
    IUserChoiceRepository
{
    public UserChoiceRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.UserChoice,App.Domain.UserChoice> mapper) 
        : base(dbContext, mapper)
    {
    }
    

    public async Task<UserChoice> GetWithLogic(UserChoice entity)
    {
        var quizId = await RepoDbContext.UserQuizzes.Where(m => m.Id == entity.UserQuizId).Select(m => m.QuizId).FirstOrDefaultAsync();
        var ids = RepoDbSet.Where(m => m.UserQuizId == entity.UserQuizId).Select(m => m.QuestionId).ToList();
        var questionsLasted = RepoDbContext.Questions
            .Where(m => m.QuizId == quizId && !ids.Contains(m.Id)).ToList();
        if (questionsLasted.Count > 0)
        {
            entity.QuestionId = questionsLasted.First().Id;
            entity.QuizId = quizId;
        }
        
        return entity;
    }
}