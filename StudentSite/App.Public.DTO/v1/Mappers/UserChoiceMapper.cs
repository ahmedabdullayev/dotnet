using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserChoiceMapper: BaseMapper<Public.DTO.v1.UserChoice, App.BLL.DTO.UserChoice>
{
    public UserChoiceMapper(IMapper mapper) : base(mapper)
    {
    }
}