using App.Contracts.BLL;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// Api controller for UserChoices
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class UserChoiceController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    /// <summary>
    /// UserChoice constructor
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="mapper"></param>
    public UserChoiceController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    /// <summary>
    /// Return Userchoices (not used)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.UserChoice>), StatusCodes.Status200OK)]
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.UserChoice>>> GetUserChoices()
    {
        return Ok((await _bll.UserChoices.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.UserChoice, App.Public.DTO.v1.UserChoice>(e)));
    }
    //logic to get next questions
    /// <summary>
    /// Get next questions with answers
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.UserChoice>> PostGetUserChoice(App.Public.DTO.v1.UserChoice entity)
    {
        var getEntity = await _bll.UserChoices.GetWithLogic(
            _mapper.Map<App.Public.DTO.v1.UserChoice, App.BLL.DTO.UserChoice>(entity), User.GetUserId());

        var returnedEntity = _mapper.Map<App.BLL.DTO.UserChoice, App.Public.DTO.v1.UserChoice>(getEntity);
        return CreatedAtAction("GetUserChoice", new { id = returnedEntity.Id }, returnedEntity);
    }
    // GET: api/Subjects/5
    /// <summary>
    /// Get UserChoice (not used)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.UserChoice), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<App.Public.DTO.v1.UserChoice>> GetUserChoice(Guid id)
    {
        var entity = await _bll.UserChoices.FirstOrDefaultAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.UserChoice, App.Public.DTO.v1.UserChoice>(entity);
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update user choice(not used)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> PutUserChoice(Guid id, App.Public.DTO.v1.UserChoice entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        
        var entityFromDb = await _bll.UserChoices.FirstOrDefaultAsync(id);
        if (entityFromDb == null)
        {
            return NotFound();
        }
        _bll.UserChoices.Update(_mapper.Map<App.Public.DTO.v1.UserChoice, App.BLL.DTO.UserChoice>(entity));
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // POST: api/Subjects
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Create User choice
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.UserChoice>> PostUserChoice(App.Public.DTO.v1.UserChoice entity)
    {
        var addEntity = _bll.UserChoices.AddWithUser(
            _mapper.Map<App.Public.DTO.v1.UserChoice, App.BLL.DTO.UserChoice>(entity), User.GetUserId());
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.UserChoice, App.Public.DTO.v1.UserChoice>(addEntity);
        
        return CreatedAtAction("GetUserChoice", new { id = savedEntity.Id }, savedEntity);
    }
    //
    // // DELETE: api/Subjects/5
    /// <summary>
    /// Remove user choice
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUserChoice(Guid id)
    {
        var entity = await _bll.UserChoices.FirstOrDefaultAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
            
        _bll.UserChoices.Remove(entity);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}