using App.Contracts.BLL;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// Api controller for Topics
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TopicController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    /// <summary>
    /// Topic constructor
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="mapper"></param>
    public TopicController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    /// <summary>
    /// Return topics
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Topic>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Topic>>> GetTopics()
    {
        return Ok((await _bll.Topics.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.Topic, App.Public.DTO.v1.Topic>(e)));
    }

    // GET: api/Subjects/5
    /// <summary>
    /// Get one topic by id(not used)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Topic), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.Topic>> GetTopic(Guid id)
    {
        var entity = await _bll.Topics.FirstOrDefaultAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.Topic, App.Public.DTO.v1.Topic>(entity);
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update topic by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutTopic(Guid id, App.Public.DTO.v1.Topic entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        
        var entityFromDb = await _bll.Topics.FirstOrDefaultAsync(id);
        if (entityFromDb == null)
        {
            return NotFound();
        }
        _bll.Topics.Update(_mapper.Map<App.Public.DTO.v1.Topic, App.BLL.DTO.Topic>(entity));
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // POST: api/Subjects
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Create topic
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.Topic>> PostTopic(App.Public.DTO.v1.Topic entity)
    {
        var addEntity = _bll.Topics.Add(_mapper.Map<App.Public.DTO.v1.Topic, App.BLL.DTO.Topic>(entity));
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.Topic, App.Public.DTO.v1.Topic>(addEntity);
        
        return CreatedAtAction("GetTopic", new { id = savedEntity.Id }, savedEntity);
    }
    //
    // // DELETE: api/Subjects/5
    /// <summary>
    /// Delete topic by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTopic(Guid id)
    {
        var entity = await _bll.Topics.FirstOrDefaultAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
            
        _bll.Topics.Remove(entity);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}