using App.BLL.Mappers;
using App.BLL.Mappers.Identity;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL: BaseBll<IAppUnitOfWork> ,IAppBLL
{
    private readonly AutoMapper.IMapper _mapper;

    protected IAppUnitOfWork UnitOfWork;
    public AppBLL(IAppUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public override async Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }

    // private IAppUserService? _users;
    // public IAppUserService Users =>
    // _users ?? = new AppUserService(UnitOfWork)

    private IAppUserService? _appUsers;
    public IAppUserService AppUsers =>
        _appUsers ??= new AppUserService(UnitOfWork.AppUsers, new AppUserMapper(_mapper));

    private IAnswerService? _answers;
    public IAnswerService Answers =>
        _answers ??= new AnswerService(UnitOfWork.Answers, new AnswerMapper(_mapper));

    private IQuestionService? _questions;
    public IQuestionService Questions =>
        _questions ??= new QuestionService(UnitOfWork.Questions, new QuestionMapper(_mapper));

    private IQuizService? _quizzes;
    public IQuizService Quizzes =>
        _quizzes ??= new QuizService(UnitOfWork.Quizzes, new QuizMapper(_mapper));

    private ISubjectService? _subjects;
    public ISubjectService Subjects =>
        _subjects ??= new SubjectService(UnitOfWork.Subjects, new SubjectMapper(_mapper));

    private ITodoService? _todos;
    public ITodoService Todos =>
        _todos ??= new TodoService(UnitOfWork.Todos, new TodoMapper(_mapper));

    private ITopicService? _topics;
    public ITopicService Topics =>
        _topics ??= new TopicService(UnitOfWork.Topics, new TopicMapper(_mapper));

    private IUserChoiceService? _userChoices;
    public IUserChoiceService UserChoices =>
        _userChoices ??= new UserChoiceService(UnitOfWork.UserChoices, new UserChoiceMapper(_mapper));

    private IUserCommentService? _userComments;
    public IUserCommentService UserComments =>
        _userComments ??= new UserCommentService(UnitOfWork.UserComments, new UserCommentMapper(_mapper));

    private IUserPostService? _userPosts;
    public IUserPostService UserPosts =>
        _userPosts ??= new UserPostService(UnitOfWork.UserPosts, new UserPostMapper(_mapper));

    private IUserQuizService? _userQuizzes;
    public IUserQuizService UserQuizzes =>
        _userQuizzes ??= new UserQuizService(UnitOfWork.UserQuizzes, new UserQuizMapper(_mapper));

}