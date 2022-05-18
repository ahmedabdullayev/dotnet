using App.Contracts.BLL;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// Api controller for Todos
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Consumes("application/json")]
[Produces("application/json")]
[Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// USE BLL AND DTO FRO REST
public class TodoController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    /// <summary>
    /// Todoo CTOR
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="mapper"></param>
    public TodoController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    /// <summary>
    /// Get todos of user by user id
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Todo>>> GetTodos()
    {
        return Ok((await _bll.Todos.GetAllAsync(User.GetUserId()))
            .Select(e => _mapper.Map<App.BLL.DTO.Todo, App.Public.DTO.v1.Todo>(e)));
    }

    // GET: api/Subjects/5
    /// <summary>
    /// Get one todoo of user by user id (not used)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Todo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.Todo>> GetTodo(Guid id)
    {
        var entity = await _bll.Todos.FirstWithUser(id, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.Todo, App.Public.DTO.v1.Todo>(entity);
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update todoo of user by user id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutTodo(Guid id, App.Public.DTO.v1.Todo entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        //secured
        var entityFromDb = await _bll.Todos.FirstWithUser(id, User.GetUserId());
        if (entityFromDb == null)
        {
            return NotFound();
        }
        
        _bll.Todos.UpdateWithUser(_mapper.Map<App.Public.DTO.v1.Todo,App.BLL.DTO.Todo>(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // POST: api/Subjects
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Create Todoo of user
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.Todo>> PostTodo(App.Public.DTO.v1.Todo entity)
    {
        var addEntity = _bll.Todos.AddWithUser(_mapper.Map<App.Public.DTO.v1.Todo, App.BLL.DTO.Todo>(entity), User.GetUserId());
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.Todo, App.Public.DTO.v1.Todo>(addEntity);
        
        return CreatedAtAction("GetTodo", new { id = savedEntity.Id }, savedEntity);
    }
    //
    /// <summary>
    /// Delete todoo by id (only user can his own)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTodo(Guid id)
    {
        //secured
        var entity = await _bll.Todos.FirstWithUser(id, User.GetUserId());
        if (entity == null)
        {
            return NotFound();
        }
            
        _bll.Todos.Remove(entity);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}