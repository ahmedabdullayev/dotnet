using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebApp.ApiControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
// [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// TODO USE BLL AND DTO FRO REST
public class SubjectsController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    public SubjectsController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    [HttpGet]
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