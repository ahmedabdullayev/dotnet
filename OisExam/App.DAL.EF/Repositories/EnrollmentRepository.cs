using App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class EnrollmentRepository : BaseRepository<App.Domain.Enrollment, AppDbContext>
{
    public EnrollmentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Enrollment>> GetAllAsyncWithUserHws(bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        query = query.Include(p => p.AppUser)
            .Include(s => s.Subject)
            .Include(se => se.Semester)
            .Include(k => k.Homeworks);

        return await query.ToListAsync();
    }
    
    public async Task<IEnumerable<Enrollment>> GetAllAsyncWithHws(bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        query = query.Include(p => p.Homeworks);

        return await query.ToListAsync();
    }
    
    public async Task<IEnumerable<Enrollment>> GetAllAsyncWithAcceptedStudents(Guid teacherId, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
    
        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        var semestersGuids = query.Where(m => m.AppUserId == teacherId).Select(m => m.SemesterId).ToList();
        var subjectGuids = query.Where(m => m.AppUserId == teacherId).Select(m => m.SubjectId).ToList();

        query = query.Where(m => semestersGuids.Contains(m.SemesterId) && subjectGuids.Contains(m.SubjectId));
        query = query.Where(m => m.IsAccepted && m.IsTeacher == false)
            .Include(p => p.AppUser)
                .Include(s => s.Subject)
                .Include(se => se.Semester)
                .Include(k => k.Homeworks);

        return await query.ToListAsync();
    }
    
    public async Task<IEnumerable<Enrollment>> GetAllAsyncWithTeachers(bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
    
        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        query = query.Where(m => m.IsTeacher == true)
            .Include(p => p.AppUser)
            .Include(s => s.Subject)
            .Include(se => se.Semester)
            .Include(k => k.Homeworks);

        return await query.ToListAsync();
    }
    public async Task<bool> CheckForSameData(App.Public.DTO.v1.Enrollment entity, Guid userId)
    {
        var query = await RepoDbContext.Enrollments.Where(m => m.AppUserId == userId
                                                         && m.SubjectId == entity.SubjectId
                                                         && m.SemesterId == entity.SemesterId).AnyAsync();
        
        return query;
    }
}