using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    IAnswerRepository Answers { get; }
    IQuestionRepository Questions { get; }
    IQuizRepository Quizzes { get; }
    ISubjectRepository Subjects { get; }
    ITodoRepository Todos { get; }
    ITopicRepository Topics { get; }
    IUserChoiceRepository UserChoices { get; }
    IUserCommentRepository UserComments { get; }
    IUserPostRepository UserPosts { get; }
    IUserQuizRepository UserQuizzes { get; }
}