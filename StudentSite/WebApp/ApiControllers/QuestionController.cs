using App.Contracts.BLL;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// TODO USE BLL AND DTO FRO REST
public class QuestionController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    public QuestionController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Question>>> GetQuestions()
    {
        return Ok((await _bll.Questions.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.Question, App.Public.DTO.v1.Question>(e)));
    }

    // GET: api/Subjects/5
    [HttpGet("{id}")]
    public async Task<ActionResult<App.Public.DTO.v1.Question>> GetQuestion(Guid id)
    {
        var entity = await _bll.Questions.FirstOrDefaultAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.Question, App.Public.DTO.v1.Question>(entity);
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuestion(Guid id, App.Public.DTO.v1.Question entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        
        var entityFromDb = await _bll.Questions.FirstOrDefaultAsync(id);
        if (entityFromDb == null)
        {
            return NotFound();
        }
        _bll.Questions.Update(_mapper.Map<App.Public.DTO.v1.Question,App.BLL.DTO.Question>(entity));
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // POST: api/Subjects
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult<App.Public.DTO.v1.Question>> PostQuestion(App.Public.DTO.v1.Question entity)
    {
        var addEntity = _bll.Questions.Add(_mapper.Map<App.Public.DTO.v1.Question, App.BLL.DTO.Question>(entity));
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.Question, App.Public.DTO.v1.Question>(addEntity);
        
        return CreatedAtAction("GetQuestion", new { id = savedEntity.Id }, savedEntity);
    }
    //
    // // DELETE: api/Subjects/5
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestion(Guid id)
    {
        var entity = await _bll.Questions.FirstOrDefaultAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
            
        _bll.Questions.Remove(entity);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}