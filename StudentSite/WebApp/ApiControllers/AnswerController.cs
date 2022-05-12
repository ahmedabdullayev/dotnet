using App.Contracts.BLL;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
// [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// TODO USE BLL AND DTO FRO REST
public class AnswerController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;

    public AnswerController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = mapper;
    }

    // GET: api/Subjects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Answer>>> GetAnswers()
    {
        return Ok((await _bll.Answers.GetAllAsync())
            .Select(e => _mapper.Map<App.BLL.DTO.Answer, App.Public.DTO.v1.Answer>(e)));
    }

    // GET: api/Subjects/5
    [HttpGet("{id}")]
    public async Task<ActionResult<App.Public.DTO.v1.Answer>> GetAnswer(Guid id)
    {
        var entity = await _bll.Answers.FirstOrDefaultAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<App.BLL.DTO.Answer, App.Public.DTO.v1.Answer>(entity);
    }

    // PUT: api/Subjects/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAnswer(Guid id, App.Public.DTO.v1.Answer entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }
        
        var entityFromDb = await _bll.Answers.FirstOrDefaultAsync(id);
        if (entityFromDb == null)
        {
            return NotFound();
        }
        _bll.Answers.Update(_mapper.Map<App.Public.DTO.v1.Answer,App.BLL.DTO.Answer>(entity));
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    //
    // // POST: api/Subjects
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<App.Public.DTO.v1.Answer>> PostAnswer(App.Public.DTO.v1.Answer entity)
    {
        var addEntity = _bll.Answers.Add(_mapper.Map<App.Public.DTO.v1.Answer, App.BLL.DTO.Answer>(entity));
        await _bll.SaveChangesAsync();

        var savedEntity = _mapper.Map<App.BLL.DTO.Answer, App.Public.DTO.v1.Answer>(addEntity);
        
        return CreatedAtAction("GetAnswer", new { id = savedEntity.Id }, savedEntity);
    }
    //
    // // DELETE: api/Subjects/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnswer(Guid id)
    {
        var entity = await _bll.Answers.FirstOrDefaultAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
            
        _bll.Answers.Remove(entity);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}