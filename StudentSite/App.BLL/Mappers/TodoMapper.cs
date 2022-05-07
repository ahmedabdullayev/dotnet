using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class TodoMapper: BaseMapper<App.BLL.DTO.Todo, App.DAL.DTO.Todo>
{
    public TodoMapper(IMapper mapper) : base(mapper)
    {
    }
}