using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork{
    private readonly AutoMapper.IMapper _mapper;
    public AppUOW(AppDbContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private IAnswerRepository? _answers;
    public virtual IAnswerRepository Answers =>
        _answers ??= new AnswerRepository(UOWDbContext, new AnswerMapper(_mapper));

    private IQuestionRepository? _questions;
    public virtual IQuestionRepository Questions =>
        _questions ??= new QuestionRepository(UOWDbContext, new QuestionMapper(_mapper));
    
    private IQuizRepository? _quizzes;
    public virtual IQuizRepository Quizzes => 
        _quizzes ??= new QuizRepository(UOWDbContext, new QuizMapper(_mapper));
    
    private ISubjectRepository? _subjects;
    public virtual ISubjectRepository Subjects =>
        _subjects ??= new SubjectRepository(UOWDbContext, new SubjectMapper(_mapper));

    private ITodoRepository? _todos;
    public virtual ITodoRepository Todos =>
        _todos ??= new TodoRepository(UOWDbContext, new TodoMapper(_mapper));
    
    private ITopicRepository? _topics;
    public virtual ITopicRepository Topics =>
        _topics ??= new TopicRepository(UOWDbContext, new TopicMapper(_mapper));

    private IUserChoiceRepository? _userChoices;
    public virtual IUserChoiceRepository UserChoices =>
        _userChoices ??= new UserChoiceRepository(UOWDbContext, new UserChoiceMapper(_mapper));

    private IUserCommentRepository? _userComments;
    public virtual IUserCommentRepository UserComments =>
        _userComments ??= new UserCommentRepository(UOWDbContext, new UserCommentMapper(_mapper));

    private IUserPostRepository? _userPosts;
    public virtual IUserPostRepository UserPosts =>
        _userPosts ??= new UserPostRepository(UOWDbContext, new UserPostMapper(_mapper));

    private IUserQuizRepository? _userQuizzes;

    public virtual IUserQuizRepository UserQuizzes =>
        _userQuizzes ??= new UserQuizRepository(UOWDbContext, new UserQuizMapper(_mapper));
}