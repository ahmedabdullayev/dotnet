using App.Contracts.BLL.Services;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    IAnswerService Answers { get; }
    IQuestionService Questions { get; }
    IQuizService Quizzes { get; }
    ISubjectService Subjects { get; }
    ITodoService Todos { get; }
    ITopicService Topics { get; }
    IUserChoiceService UserChoices { get; }
    IUserCommentService UserComments { get; }
    IUserPostService UserPosts { get; }
    IUserQuizService UserQuizzes { get; }
    
}