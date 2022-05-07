using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class QuestionMapper: BaseMapper<App.DAL.DTO.Question, App.Domain.Question>
{
    public QuestionMapper(IMapper mapper) : base(mapper)
    {
    }
}