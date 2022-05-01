using App.Contracts.DAL;
using App.DAL.EF.Repositories;

namespace App.DAL.EF;

public class AppUOW : IAppUnitOfWork
{
    protected readonly AppDbContext UOWDbContext;
    public AppUOW(AppDbContext uowDbContext)
    {
        UOWDbContext = uowDbContext;
    }
    public virtual async Task<int> SaveChangesAsync()
    {
        return await UOWDbContext.SaveChangesAsync();
    }

    public virtual int SaveChanges()
    {
        return UOWDbContext.SaveChanges();
    }

    private ISubjectRepository? _subjects;
    public virtual ISubjectRepository Subjects =>
        _subjects ??= new SubjectRepository(UOWDbContext);

    private IQuizRepository? _quizRepository;
    public virtual IQuizRepository Quizzes => 
        _quizRepository ??= new QuizRepository(UOWDbContext);
}