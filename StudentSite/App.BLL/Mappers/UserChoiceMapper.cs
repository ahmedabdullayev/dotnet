using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserChoiceMapper: BaseMapper<App.BLL.DTO.UserChoice, App.DAL.DTO.UserChoice>
{
    public UserChoiceMapper(IMapper mapper) : base(mapper)
    {
    }
}