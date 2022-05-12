using App.Contracts.BLL;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class UserQuizController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    public UserQuizController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.UserQuiz>>> GetUserQuizzes()
    {
        return Ok((await _bll.UserQuizzes.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.UserQuiz, App.Public.DTO.v1.UserQuiz>(e)));
    }

    // GET: api/Subjects/5
    [HttpGet("{id}")]
    public async Task<ActionResult<App.Public.DTO.v1.UserQuiz>> GetUserQuiz(Guid id)
    {
        var entity = await _bll.UserQuizzes.FirstOrDefaultAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.UserQuiz, App.Public.DTO.v1.UserQuiz>(entity);
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserQuiz(Guid id, App.Public.DTO.v1.UserQuiz entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        
        var entityFromDb = await _bll.UserQuizzes.FirstOrDefaultAsync(id);
        if (entityFromDb == null)
        {
            return NotFound();
        }
        _bll.UserQuizzes.Update(_mapper.Map<App.Public.DTO.v1.UserQuiz, App.BLL.DTO.UserQuiz>(entity));
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // POST: api/Subjects
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<App.Public.DTO.v1.UserQuiz>> PostUserQuiz(App.Public.DTO.v1.UserQuiz entity)
    {
        var addEntity = _bll.UserQuizzes.Add(_mapper.Map<App.Public.DTO.v1.UserQuiz, App.BLL.DTO.UserQuiz>(entity));
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.UserQuiz, App.Public.DTO.v1.UserQuiz>(addEntity);
        
        return CreatedAtAction("GetUserQuiz", new { id = savedEntity.Id }, savedEntity);
    }
    //
    // // DELETE: api/Subjects/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserQuiz(Guid id)
    {
        var entity = await _bll.UserQuizzes.FirstOrDefaultAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
            
        _bll.UserQuizzes.Remove(entity);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}