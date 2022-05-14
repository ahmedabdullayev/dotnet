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
public class UserChoiceController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    public UserChoiceController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    [HttpGet]
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.UserChoice>>> GetUserChoices()
    {
        return Ok((await _bll.UserChoices.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.UserChoice, App.Public.DTO.v1.UserChoice>(e)));
    }
    //logic to get next questions
    [HttpPost]
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<App.Public.DTO.v1.UserChoice>> PostGetUserChoice(App.Public.DTO.v1.UserChoice entity)
    {
        var getEntity = await _bll.UserChoices.GetWithLogic(
            _mapper.Map<App.Public.DTO.v1.UserChoice, App.BLL.DTO.UserChoice>(entity), User.GetUserId());

        var returnedEntity = _mapper.Map<App.BLL.DTO.UserChoice, App.Public.DTO.v1.UserChoice>(getEntity);
        return CreatedAtAction("GetUserChoice", new { id = returnedEntity.Id }, returnedEntity);
    }
    // GET: api/Subjects/5
    [HttpGet("{id}")]
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    [HttpPut("{id}")]
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
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
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
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
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