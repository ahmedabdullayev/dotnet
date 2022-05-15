using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// USE BLL AND DTO FRO REST
public class QuizController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    public QuizController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Quiz>>> GetQuizzes()
    {
        return Ok((await _bll.Quizzes.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.Quiz, App.Public.DTO.v1.Quiz>(e)));
    }
    
    
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Quiz>>> GetQuizzesBySubject(Guid id)
    {
        return Ok((await _bll.Quizzes.GetAllAsyncBySubject(id))
            .Select(e => _mapper.Map<App.BLL.DTO.Quiz, App.Public.DTO.v1.Quiz>(e)));
    }

    // GET: api/Subjects/5
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
    public async Task<ActionResult<App.Public.DTO.v1.Quiz>> GetQuiz(Guid id)
    {
        var entity = await _bll.Quizzes.FirstOrDefaultAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.Quiz, App.Public.DTO.v1.Quiz>(entity);
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuiz(Guid id, App.Public.DTO.v1.Quiz entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        
        var entityFromDb = await _bll.Quizzes.FirstOrDefaultAsync(id);
        if (entityFromDb == null)
        {
            return NotFound();
        }
        _bll.Quizzes.Update(_mapper.Map<App.Public.DTO.v1.Quiz,App.BLL.DTO.Quiz>(entity));
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // POST: api/Subjects
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult<App.Public.DTO.v1.Quiz>> PostQuiz(App.Public.DTO.v1.Quiz entity)
    {
        var addEntity = _bll.Quizzes.Add(_mapper.Map<App.Public.DTO.v1.Quiz, App.BLL.DTO.Quiz>(entity));
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.Quiz, App.Public.DTO.v1.Quiz>(addEntity);
        
        return CreatedAtAction("GetQuiz", new { id = savedEntity.Id }, savedEntity);
    }
    //
    // // DELETE: api/Subjects/5
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuiz(Guid id)
    {
        var entity = await _bll.Quizzes.FirstOrDefaultAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
            
        _bll.Quizzes.Remove(entity);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}