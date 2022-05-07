using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class TodoService : BaseEntityService<App.BLL.DTO.Todo, App.DAL.DTO.Todo, ITodoRepository>,
    ITodoService
{
    public TodoService(ITodoRepository repository, IMapper<Todo, DAL.DTO.Todo> mapper) : base(repository, mapper)
    {
    }
}