using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class AnswerMapper : BaseMapper<App.BLL.DTO.Answer, App.DAL.DTO.Answer>
{
    public AnswerMapper(IMapper mapper) : base(mapper)
    {
    }
}