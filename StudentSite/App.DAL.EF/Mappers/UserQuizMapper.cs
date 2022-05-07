using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserQuizMapper: BaseMapper<App.DAL.DTO.UserQuiz, App.Domain.UserQuiz>
{
    public UserQuizMapper(IMapper mapper) : base(mapper)
    {
    }
}