using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class AnswerMapper : BaseMapper<App.DAL.DTO.Answer, App.Domain.Answer>
{
    public AnswerMapper(IMapper mapper) : base(mapper)
    {
    }
}