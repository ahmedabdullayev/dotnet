using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

/// <summary>
/// Api controller for Quizzes
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// USE BLL AND DTO FRO REST
public class QuizController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    /// <summary>
    /// Quiz CTOR
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="mapper"></param>
    public QuizController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    /// <summary>
    /// Return all quizzes
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Quiz>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Quiz>>> GetQuizzes()
    {
        return Ok((await _bll.Quizzes.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.Quiz, App.Public.DTO.v1.Quiz>(e)));
    }
    
    
    /// <summary>
    /// Get quizzes by subject(but we just use Subject controller with collection of quizzes)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Quiz), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Quiz>>> GetQuizzesBySubject(Guid id)
    {
        return Ok((await _bll.Quizzes.GetAllAsyncBySubject(id))
            .Select(e => _mapper.Map<App.BLL.DTO.Quiz, App.Public.DTO.v1.Quiz>(e)));
    }

    /// <summary>
    /// Return one quiz (not used)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Quiz), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.Quiz>> GetQuiz(Guid id)
    {
        var entity = await _bll.Quizzes.FirstOrDefaultAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.Quiz, App.Public.DTO.v1.Quiz>(entity);
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update quiz(with languages for example)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Create quiz
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.Quiz>> PostQuiz(App.Public.DTO.v1.Quiz entity)
    {
        var addEntity = _bll.Quizzes.Add(_mapper.Map<App.Public.DTO.v1.Quiz, App.BLL.DTO.Quiz>(entity));
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.Quiz, App.Public.DTO.v1.Quiz>(addEntity);
        
        return CreatedAtAction("GetQuiz", new { id = savedEntity.Id }, savedEntity);
    }
    //
    /// <summary>
    /// Remove quiz by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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