using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class QuizMapper: BaseMapper<App.DAL.DTO.Quiz, App.Domain.Quiz>
{
    public QuizMapper(IMapper mapper) : base(mapper)
    {
    }
}