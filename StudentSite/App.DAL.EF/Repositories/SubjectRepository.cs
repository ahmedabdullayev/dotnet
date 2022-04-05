using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class SubjectRepository : BaseEntityRepository<Subject, AppDbContext>, ISubjectRepository
{
    public SubjectRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}