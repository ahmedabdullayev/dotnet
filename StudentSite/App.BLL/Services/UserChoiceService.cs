using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserChoiceService : BaseEntityService<App.BLL.DTO.UserChoice, App.DAL.DTO.UserChoice, IUserChoiceRepository>,
    IUserChoiceService
{
    public UserChoiceService(IUserChoiceRepository repository, IMapper<UserChoice, DAL.DTO.UserChoice> mapper) : base(repository, mapper)
    {
    }

    public async Task<UserChoice> GetWithLogic(UserChoice entity,  Guid userId)
    {
        return Mapper.Map(await Repository.GetWithLogic(Mapper.Map(entity)!, userId))!;
    }

    public UserChoice AddWithUser(UserChoice entity, Guid userId)
    {
        return Mapper.Map(Repository.AddWithUser(Mapper.Map(entity)!, userId))!;
    }
}