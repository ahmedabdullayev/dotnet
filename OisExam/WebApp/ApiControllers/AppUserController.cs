using App.DAL.EF;
using App.Public.DTO.v1.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Consumes("application/json")]
[Produces("application/json")]
public class AppUserController: ControllerBase
{
    private readonly AppUOW _uow;
    private readonly AppUserMapper _mapper;
    private readonly AppDbContext _context;
    
    public AppUserController(AppUOW uow, AppDbContext context)
    {
        _uow = uow;
        _context = context;
        _mapper = new AppUserMapper();
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Identity.AppUser>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Identity.AppUser>>> GetUsers()
    {
        return Ok((await _uow.AppUsers.GetAllAsync()).Select(a => _mapper.MapToPublic(a)));
    }
}