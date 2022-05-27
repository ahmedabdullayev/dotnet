using App.DAL.EF;
using App.Public.DTO.v1.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(Roles = "admin, teacher",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Consumes("application/json")]
[Produces("application/json")]
public class HomeworkController: ControllerBase
{
    private readonly AppUOW _uow;
    private readonly HomeworkMapper _mapper;

    public HomeworkController(AppDbContext context, AppUOW uow)
    {
        _uow = uow;
        _mapper = new HomeworkMapper();
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Homework>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Homework>>> GetHomeworks()
    {
        return Ok((await _uow.Homeworks.GetAllAsync()).Select(a => _mapper.MapToPublic(a)));
    }
    
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Homework), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.Homework>> GetHomework(Guid id)
    {
        var homework = await _uow.Homeworks.FirstOrDefaultAsync(id);

        if (homework == null)
        {
            return NotFound();
        }

        return _mapper.MapToPublic(homework);
    }
    
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutHomework(Guid id, App.Public.DTO.v1.Homework homework)
    {
        
        if (id != homework.Id)
        {
            return BadRequest();
        }

        _uow.Homeworks.UpdateGrade(id,_mapper.MapFromPublic(homework));
        await _uow.SaveChangesAsync();
            
        return NoContent();
    }
    
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.Homework>> PostHomework(App.Public.DTO.v1.Homework homework)
    {
        var savedEntity = _uow.Homeworks.Add(_mapper.MapFromPublic(homework));
        await _uow.SaveChangesAsync();
        var returnEntity = _mapper.MapToPublic(savedEntity);

        return CreatedAtAction("GetHomework", new { id = returnEntity.Id }, returnEntity);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteHomework(Guid id)
    {
        var question = await _uow.Homeworks.FirstOrDefaultAsync(id);
        if (question == null)
        {
            return NotFound();
        }

        _uow.Homeworks.Remove(question);
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}