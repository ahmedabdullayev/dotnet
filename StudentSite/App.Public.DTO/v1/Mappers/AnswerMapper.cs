using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class AnswerMapper : BaseMapper<Public.DTO.v1.Answer, App.BLL.DTO.Answer>
{
    public AnswerMapper(IMapper mapper) : base(mapper)
    {
    }
}