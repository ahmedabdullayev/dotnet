using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    ISubjectRepository Subjects { get; }
    IQuizRepository Quizzes { get; }
}