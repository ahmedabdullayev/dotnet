using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class SubjectRepository : BaseEntityRepository<App.DAL.DTO.Subject, App.Domain.Subject, AppDbContext>, ISubjectRepository
{
    public SubjectRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Subject,App.Domain.Subject> mapper) 
        : base(dbContext, mapper)
    {
    }
}