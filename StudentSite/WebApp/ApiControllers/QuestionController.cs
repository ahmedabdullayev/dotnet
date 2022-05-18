using App.Contracts.BLL;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// Api controller for Questions
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Consumes("application/json")]
[Produces("application/json")]
[Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// USE BLL AND DTO FRO REST
public class QuestionController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    /// <summary>
    /// Question CTOR
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="mapper"></param>
    public QuestionController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    /// <summary>
    /// Return questions with answers(not used)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Question>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Question>>> GetQuestions()
    {
        return Ok((await _bll.Questions.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.Question, App.Public.DTO.v1.Question>(e)));
    }

    /// <summary>
    /// Get question by id with answers
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Question), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.Question>> GetQuestion(Guid id)
    {
        var entity = await _bll.Questions.FirstOrDefaultAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.Question, App.Public.DTO.v1.Question>(entity);
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update a question by id(for ex. languages)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuestion(Guid id, App.Public.DTO.v1.Question entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        
        var entityFromDb = await _bll.Questions.FirstOrDefaultAsync(id);
        if (entityFromDb == null)
        {
            return NotFound();
        }
        _bll.Questions.Update(_mapper.Map<App.Public.DTO.v1.Question,App.BLL.DTO.Question>(entity));
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Create question
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.Question>> PostQuestion(App.Public.DTO.v1.Question entity)
    {
        var addEntity = _bll.Questions.Add(_mapper.Map<App.Public.DTO.v1.Question, App.BLL.DTO.Question>(entity));
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.Question, App.Public.DTO.v1.Question>(addEntity);
        
        return CreatedAtAction("GetQuestion", new { id = savedEntity.Id }, savedEntity);
    }
    //
    // // DELETE: api/Subjects/5
    /// <summary>
    /// Remove question by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Question), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteQuestion(Guid id)
    {
        var entity = await _bll.Questions.FirstOrDefaultAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
            
        _bll.Questions.Remove(entity);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}