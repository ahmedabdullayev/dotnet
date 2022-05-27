using App.DAL.EF;
using App.Public.DTO.v1.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Consumes("application/json")]
[Produces("application/json")]
[Authorize(Roles = "admin, student, teacher",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SubjectController : ControllerBase
{
    private readonly AppUOW _uow;
    private readonly SubjectMapper _mapper;

    public SubjectController(AppUOW uow)
    {
        _uow = uow;
        _mapper = new SubjectMapper();
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Subject>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Subject>>> GetSubjects()
    {
        return Ok((await _uow.Subjects.GetAllAsync()).Select(a => _mapper.MapToPublic(a)));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Subject), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.Subject>> GetSubject(Guid id)
    {
        var subject = await _uow.Subjects.FirstOrDefaultAsync(id);

        if (subject == null)
        {
            return NotFound();
        }

        return _mapper.MapToPublic(subject);
    }
    
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutSubject(Guid id, App.Public.DTO.v1.Subject subject)
    {
        if (id != subject.Id)
        {
            return BadRequest();
        }

        _uow.Subjects.Update(_mapper.MapFromPublic(subject));
        await _uow.SaveChangesAsync();
            
        return NoContent();
    }
    
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.Subject>> PostSubject(App.Public.DTO.v1.Subject subject)
    {
        var savedEntity = _uow.Subjects.Add(_mapper.MapFromPublic(subject));
        await _uow.SaveChangesAsync();
        var returnEntity = _mapper.MapToPublic(savedEntity);

        return CreatedAtAction("GetSubject", new { id = returnEntity.Id }, returnEntity);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        var subject = await _uow.Subjects.FirstOrDefaultAsync(id);
        if (subject == null)
        {
            return NotFound();
        }

        _uow.Subjects.Remove(subject);
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}