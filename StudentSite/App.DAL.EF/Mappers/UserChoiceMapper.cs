using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserChoiceMapper: BaseMapper<App.DAL.DTO.UserChoice, App.Domain.UserChoice>
{
    public UserChoiceMapper(IMapper mapper) : base(mapper)
    {
    }
}