using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class QuestionMapper: BaseMapper<App.BLL.DTO.Question, App.DAL.DTO.Question>
{
    public QuestionMapper(IMapper mapper) : base(mapper)
    {
    }
}