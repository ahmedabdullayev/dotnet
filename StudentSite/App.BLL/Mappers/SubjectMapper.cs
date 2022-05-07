using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class SubjectMapper: BaseMapper<App.BLL.DTO.Subject, App.DAL.DTO.Subject>
{
    public SubjectMapper(IMapper mapper) : base(mapper)
    {
    }
}