using App.Contracts.BLL;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// Api controller for user post
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class UserPostController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    /// <summary>
    /// User post CTOR
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="mapper"></param>
    public UserPostController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    /// <summary>
    /// Get user posts(not used)
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.UserPost>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.UserPost>>> GetUserPosts()
    {
        return Ok((await _bll.UserPosts.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.UserPost, App.Public.DTO.v1.UserPost>(e)));
    }

    /// <summary>
    /// Return one post with comments
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.UserPost), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.UserPost>> GetUserPost(Guid id)
    {
        var entity = await _bll.UserPosts.FirstOrDefaultAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.UserPost, App.Public.DTO.v1.UserPost>(entity);
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update user post(not used)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutUserPost(Guid id, App.Public.DTO.v1.UserPost entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        
        var entityFromDb = await _bll.UserPosts.FirstOrDefaultAsync(id);
        if (entityFromDb == null)
        {
            return NotFound();
        }
        _bll.UserPosts.Update(_mapper.Map<App.Public.DTO.v1.UserPost, App.BLL.DTO.UserPost>(entity));
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // POST: api/Subjects
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Create userpost by userid
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.UserPost>> PostUserPost(App.Public.DTO.v1.UserPost entity)
    {
        var addEntity = _bll.UserPosts.AddWithUser(
            _mapper.Map<App.Public.DTO.v1.UserPost, App.BLL.DTO.UserPost>(entity), User.GetUserId());
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.UserPost, App.Public.DTO.v1.UserPost>(addEntity);
        
        return CreatedAtAction("GetUserPost", new { id = savedEntity.Id }, savedEntity);
    }
    //
    // // DELETE: api/Subjects/5
    /// <summary>
    /// Delete user post(not used)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUserPost(Guid id)
    {
        var entity = await _bll.UserPosts.FirstOrDefaultAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
            
        _bll.UserPosts.Remove(entity);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}