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
public class UserCommentController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    public UserCommentController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.UserComment>>> GetUserComments()
    {
        return Ok((await _bll.UserComments.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.UserComment, App.Public.DTO.v1.UserComment>(e)));
    }

    // GET: api/Subjects/5
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
    public async Task<ActionResult<App.Public.DTO.v1.UserComment>> GetUserComment(Guid id)
    {
        var entity = await _bll.UserComments.FirstOrDefaultAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.UserComment, App.Public.DTO.v1.UserComment>(entity);
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserComment(Guid id, App.Public.DTO.v1.UserComment entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        
        var entityFromDb = await _bll.UserComments.FirstOrDefaultAsync(id);
        if (entityFromDb == null)
        {
            return NotFound();
        }
        _bll.UserComments.Update(_mapper.Map<App.Public.DTO.v1.UserComment, App.BLL.DTO.UserComment>(entity));
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // POST: api/Subjects
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult<App.Public.DTO.v1.UserComment>> PostUserComment(App.Public.DTO.v1.UserComment entity)
    {
        var addEntity = _bll.UserComments.AddWithUser(
            _mapper.Map<App.Public.DTO.v1.UserComment, App.BLL.DTO.UserComment>(entity), User.GetUserId());
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.UserComment, App.Public.DTO.v1.UserComment>(addEntity);
        
        return CreatedAtAction("GetUserComment", new { id = savedEntity.Id }, savedEntity);
    }
    //
    // // DELETE: api/Subjects/5
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserComment(Guid id)
    {
        var entity = await _bll.UserComments.FirstOrDefaultAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
            
        _bll.UserComments.Remove(entity);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}