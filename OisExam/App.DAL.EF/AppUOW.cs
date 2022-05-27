using App.DAL.EF.Repositories;

namespace App.DAL.EF;

public class AppUOW
{
    private readonly AppDbContext _uowDbContext;
        
    public AppRoleRepository AppRoles { get; set; }
    public AppUserRepository AppUsers { get; set; }
    public SubjectRepository Subjects { get; set; }
    public SemesterRepository Semesters { get; set; }
    public EnrollmentRepository Enrollments { get; set; }
    public HomeworkRepository Homeworks { get; set; }

    public AppUOW(AppDbContext uowDbContext)
    {
        _uowDbContext = uowDbContext;
        AppRoles = new AppRoleRepository(uowDbContext);
        AppUsers = new AppUserRepository(uowDbContext);
        Subjects = new SubjectRepository(uowDbContext); 
        Semesters = new SemesterRepository(uowDbContext);
        Enrollments = new EnrollmentRepository(uowDbContext);
        Homeworks = new HomeworkRepository(uowDbContext);
    }
    
    public Task<int> SaveChangesAsync()
    {
        return _uowDbContext.SaveChangesAsync();
    }
}