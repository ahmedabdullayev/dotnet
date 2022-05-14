using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserPostService : BaseEntityService<App.BLL.DTO.UserPost, App.DAL.DTO.UserPost, IUserPostRepository>,
    IUserPostService
{
    public UserPostService(IUserPostRepository repository, IMapper<UserPost, DAL.DTO.UserPost> mapper) : base(repository, mapper)
    {
    }

    public UserPost AddWithUser(UserPost entity, Guid userId)
    {
        return Mapper.Map(Repository.AddWithUser(Mapper.Map(entity)!, userId))!;
    }
}