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
// [Authorize(Roles = "admin",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SemesterController: ControllerBase
{
    private readonly AppUOW _uow;
    private readonly SemesterMapper _mapper;

    public SemesterController(AppUOW uow)
    {
        _uow = uow;
        _mapper = new SemesterMapper();
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Semester>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Semester>>> GetSemesters()
    {
        return Ok((await _uow.Semesters.GetAllAsync()).Select(a => _mapper.MapToPublic(a)));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Semester), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.Semester>> GetSemester(Guid id)
    {
        var semester = await _uow.Semesters.FirstOrDefaultAsync(id);

        if (semester == null)
        {
            return NotFound();
        }

        return _mapper.MapToPublic(semester);
    }
    
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutSemester(Guid id, App.Public.DTO.v1.Semester semester)
    {
        if (id != semester.Id)
        {
            return BadRequest();
        }

        _uow.Semesters.Update(_mapper.MapFromPublic(semester));
        await _uow.SaveChangesAsync();
            
        return NoContent();
    }
    
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.Semester>> PostSemester(App.Public.DTO.v1.Semester semester)
    {
        var savedEntity = _uow.Semesters.Add(_mapper.MapFromPublic(semester));
        await _uow.SaveChangesAsync();
        var returnEntity = _mapper.MapToPublic(savedEntity);

        return CreatedAtAction("GetSemester", new { id = returnEntity.Id }, returnEntity);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteSemester(Guid id)
    {
        var semester = await _uow.Semesters.FirstOrDefaultAsync(id);
        if (semester == null)
        {
            return NotFound();
        }

        _uow.Semesters.Remove(semester);
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}