using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserChoiceRepository: BaseEntityRepository<App.DAL.DTO.UserChoice, App.Domain.UserChoice, AppDbContext>, 
    IUserChoiceRepository
{
    public UserChoiceRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.UserChoice,App.Domain.UserChoice> mapper) 
        : base(dbContext, mapper)
    {
    }
}