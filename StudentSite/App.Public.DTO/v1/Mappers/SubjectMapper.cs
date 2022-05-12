using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class SubjectMapper: BaseMapper<Public.DTO.v1.Subject, App.BLL.DTO.Subject>
{
    public SubjectMapper(IMapper mapper) : base(mapper)
    {
    }
}