using App.Contracts.BLL;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// API Controller for Answers
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Consumes("application/json")]
[Produces("application/json")]
[Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// USE BLL AND DTO FRO REST
public class AnswerController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    /// <summary>
    /// AnswerCTOR
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="mapper"></param>
    public AnswerController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    /// <summary>
    /// Get all answers (not used)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Answer>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Answer>>> GetAnswers()
    {
        return Ok((await _bll.Answers.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.Answer, App.Public.DTO.v1.Answer>(e)));
    }

    /// <summary>
    /// Return answer(not used)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Answer), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.Answer>> GetAnswer(Guid id)
    {
        var entity = await _bll.Answers.FirstOrDefaultAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.Answer, App.Public.DTO.v1.Answer>(entity);
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update answer (not used)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutAnswer(Guid id, App.Public.DTO.v1.Answer entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        
        var entityFromDb = await _bll.Answers.FirstOrDefaultAsync(id);
        if (entityFromDb == null)
        {
            return NotFound();
        }
        _bll.Answers.Update(_mapper.Map<App.Public.DTO.v1.Answer,App.BLL.DTO.Answer>(entity));
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Create answer
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.Answer>> PostAnswer(App.Public.DTO.v1.Answer entity)
    {
        var addEntity = _bll.Answers.Add(_mapper.Map<App.Public.DTO.v1.Answer, App.BLL.DTO.Answer>(entity));
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.Answer, App.Public.DTO.v1.Answer>(addEntity);
        
        return CreatedAtAction("GetAnswer", new { id = savedEntity.Id }, savedEntity);
    }
    //
    /// <summary>
    /// Remove answer(not used)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAnswer(Guid id)
    {
        var entity = await _bll.Answers.FirstOrDefaultAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
            
        _bll.Answers.Remove(entity);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}