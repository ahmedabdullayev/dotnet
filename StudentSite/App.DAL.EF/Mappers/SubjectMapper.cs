using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class SubjectMapper: BaseMapper<App.DAL.DTO.Subject, App.Domain.Subject>
{
    public SubjectMapper(IMapper mapper) : base(mapper)
    {
    }
}