using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class SubjectService : BaseEntityService<App.BLL.DTO.Subject, App.DAL.DTO.Subject, ISubjectRepository>,
    ISubjectService
{
    public SubjectService(ISubjectRepository repository, IMapper<Subject, DAL.DTO.Subject> mapper) : base(repository, mapper)
    {
    }


}