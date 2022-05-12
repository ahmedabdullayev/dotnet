using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class TodoMapper: BaseMapper<Public.DTO.v1.Todo, App.BLL.DTO.Todo>
{
    public TodoMapper(IMapper mapper) : base(mapper)
    {
    }
}