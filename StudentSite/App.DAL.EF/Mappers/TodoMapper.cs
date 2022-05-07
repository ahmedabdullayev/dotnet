using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class TodoMapper: BaseMapper<App.DAL.DTO.Todo, App.Domain.Todo>
{
    public TodoMapper(IMapper mapper) : base(mapper)
    {
    }
}