using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class QuizMapper: BaseMapper<App.BLL.DTO.Quiz, App.DAL.DTO.Quiz>
{
    public QuizMapper(IMapper mapper) : base(mapper)
    {
    }
}