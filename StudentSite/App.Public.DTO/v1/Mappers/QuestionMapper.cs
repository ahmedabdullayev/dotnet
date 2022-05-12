using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class QuestionMapper: BaseMapper<Public.DTO.v1.Question, App.BLL.DTO.Question>
{
    public QuestionMapper(IMapper mapper) : base(mapper)
    {
    }
}