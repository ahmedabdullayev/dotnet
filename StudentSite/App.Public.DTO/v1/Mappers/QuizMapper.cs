using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class QuizMapper: BaseMapper<Public.DTO.v1.Quiz, App.BLL.DTO.Quiz>
{
    public QuizMapper(IMapper mapper) : base(mapper)
    {
    }
}