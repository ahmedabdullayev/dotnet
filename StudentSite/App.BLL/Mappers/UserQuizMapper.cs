using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserQuizMapper: BaseMapper<App.BLL.DTO.UserQuiz, App.DAL.DTO.UserQuiz>
{
    public UserQuizMapper(IMapper mapper) : base(mapper)
    {
    }
}