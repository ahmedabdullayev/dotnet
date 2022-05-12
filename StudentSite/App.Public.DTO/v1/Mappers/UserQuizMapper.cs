using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserQuizMapper: BaseMapper<Public.DTO.v1.UserQuiz, App.BLL.DTO.UserQuiz>
{
    public UserQuizMapper(IMapper mapper) : base(mapper)
    {
    }
}