using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

/// <summary>
/// API Controller for Subjects
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SubjectsController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    /// <summary>
    /// SubjectCTOR
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="mapper"></param>
    public SubjectsController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Subject>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Subject>>> GetSubjects()
    {
        return Ok((await _bll.Subjects.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.Subject, App.Public.DTO.v1.Subject>(e)));
    }
    // public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Subject>>> GetSubjects()
    // {
    //     return Ok((await _bll.Subjects.GetAllAsync())
    //         .Select(e => _mapper.Map<App.BLL.DTO.Subject, App.Public.DTO.v1.Subject>(e)));
    // }

    // GET: api/Subjects/5
    /// <summary>
    /// Get subject by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Subject), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<App.Public.DTO.v1.Subject>> GetSubject(Guid id)
    {
        var subject = await _bll.Subjects.FirstOrDefaultAsync(id);

        if (subject == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.Subject, App.Public.DTO.v1.Subject>(subject);
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    /// <summary>
    /// Update Subject
    /// </summary>
    /// <param name="id"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubject(Guid id, App.Public.DTO.v1.Subject subject)
    {
        if (id != subject.Id)
        {
            return BadRequest();
        }
        
        var subjectFromDb = await _bll.Subjects.FirstOrDefaultAsync(id);
        if (subjectFromDb == null)
        {
            return NotFound();
        }
        _bll.Subjects.Update(_mapper.Map<App.Public.DTO.v1.Subject,App.BLL.DTO.Subject>(subject));
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // POST: api/Subjects
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add subject
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<ActionResult<App.Public.DTO.v1.Subject>> PostSubject(App.Public.DTO.v1.Subject subject)
    {
        var addSubj = _bll.Subjects.Add(_mapper.Map<App.Public.DTO.v1.Subject, App.BLL.DTO.Subject>(subject));
        await _bll.SaveChangesAsync();

        var savedSubj = _mapper.Map<App.BLL.DTO.Subject, App.Public.DTO.v1.Subject>(addSubj);
        
        return CreatedAtAction("GetSubject", new { id = savedSubj.Id }, savedSubj);
    }
    //
    // // DELETE: api/Subjects/5
    /// <summary>
    /// Delete subj
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        var subj = await _bll.Subjects.FirstOrDefaultAsync(id);
        if (subj == null)
        {
            return NotFound();
        }
            
        _bll.Subjects.Remove(subj);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}