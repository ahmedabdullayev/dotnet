using App.Contracts.BLL;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// Api controller for UserQuiz 
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class UserQuizController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    /// <summary>
    /// UserQuiz ctor
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="mapper"></param>
    public UserQuizController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    /// <summary>
    /// Return userQuizzes all (not used)
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.UserQuiz>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.UserQuiz>>> GetUserQuizzes()
    {
        return Ok((await _bll.UserQuizzes.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.UserQuiz, App.Public.DTO.v1.UserQuiz>(e)));
    }

    // GET: api/Subjects/5
    /// <summary>
    /// Actually returns user result after completing quiz
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.UserQuiz), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// <summary>
    /// Update user quiz(not used)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    /// <summary>
    /// Create userquiz by user id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.UserQuiz>> PostUserQuiz(App.Public.DTO.v1.UserQuiz entity)
    {
        var addEntity = _bll.UserQuizzes.AddWithUser(_mapper.Map<App.Public.DTO.v1.UserQuiz, App.BLL.DTO.UserQuiz>(entity), User.GetUserId());
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.UserQuiz, App.Public.DTO.v1.UserQuiz>(addEntity);
        
        return CreatedAtAction("GetUserQuiz", new { id = savedEntity.Id }, savedEntity);
    }
    //
    /// <summary>
    /// Remove userquiz (not used)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.UserQuiz), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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