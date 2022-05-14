using App.Contracts.BLL;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TopicController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    public TopicController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Topic>>> GetTopics()
    {
        return Ok((await _bll.Topics.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.Topic, App.Public.DTO.v1.Topic>(e)));
    }

    // GET: api/Subjects/5
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
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
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("{id}")]
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
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult<App.Public.DTO.v1.Topic>> PostTopic(App.Public.DTO.v1.Topic entity)
    {
        var addEntity = _bll.Topics.Add(_mapper.Map<App.Public.DTO.v1.Topic, App.BLL.DTO.Topic>(entity));
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.Topic, App.Public.DTO.v1.Topic>(addEntity);
        
        return CreatedAtAction("GetTopic", new { id = savedEntity.Id }, savedEntity);
    }
    //
    // // DELETE: api/Subjects/5
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
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