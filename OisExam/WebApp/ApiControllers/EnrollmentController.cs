using App.DAL.EF;
using App.Public.DTO.v1.Mappers;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Authorize(Roles = "admin, teacher, student",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Consumes("application/json")]
[Produces("application/json")]
public class EnrollmentController: ControllerBase
{
    private readonly AppUOW _uow;
    private readonly EnrollmentMapper _mapper;
    private readonly AppDbContext _context;
    
    public EnrollmentController(AppUOW uow, AppDbContext context)
    {
        _uow = uow;
        _context = context;
        _mapper = new EnrollmentMapper();
    }
    
    [Authorize(Roles = "admin, teacher",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Enrollment>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Enrollment>>> GetEnrollments()
    {
        return Ok((await _uow.Enrollments.GetAllAsyncWithUserHws()).Select(a => _mapper.MapToPublic(a)));
    }
    
    [Authorize(Roles = "admin, teacher",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Enrollment>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Enrollment>>> GetEnrollmentsStudentAccepted()
    {
        return Ok((await _uow.Enrollments.GetAllAsyncWithAcceptedStudents()).Select(a => _mapper.MapToPublic(a)));
    }
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Enrollment>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Enrollment>>> GetEnrollmentsTeacherPublic()
    {
        return Ok((await _uow.Enrollments.GetAllAsyncWithTeachers()).Select(a => _mapper.MapToPublic(a)));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(App.Public.DTO.v1.Enrollment), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.Enrollment>> GetEnrollment(Guid id)
    {
        var enrollment = await _uow.Enrollments.FirstOrDefaultAsync(id);

        if (enrollment == null)
        {
            return NotFound();
        }

        return _mapper.MapToPublic(enrollment);
    }
    
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PutEnrollmentAccept(Guid id, App.Public.DTO.v1.Enrollment enrollment)
    {
        
        if (id != enrollment.Id)
        {
            return BadRequest();
        }
        
        
        _uow.Enrollments.Update(_mapper.MapFromPublicAccept(enrollment));
        await _uow.SaveChangesAsync();
            
        return NoContent();
    }
    
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Roles = "admin, teacher",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> PutEnrollment(Guid id, App.Public.DTO.v1.Enrollment enrollment)
    {
        
        if (id != enrollment.Id)
        {
            return BadRequest();
        }
        
        
        _uow.Enrollments.Update(_mapper.MapFromPublicAcceptNoUser(enrollment));
        await _uow.SaveChangesAsync();
            
        return NoContent();
    }
    
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.Enrollment>> PostEnrollmentUser(App.Public.DTO.v1.Enrollment enrollment)
    {
        if (await _uow.Enrollments.CheckForSameData(enrollment, User.GetUserId()))
        {
            return BadRequest("bad");
        }   
        Console.WriteLine("ID: " + User.GetUserId());
        var domainData = new App.Domain.Enrollment()
        {
            IsTeacher = false,
            IsAccepted = false,
            AppUserId = User.GetUserId(),
            SemesterId = enrollment.SemesterId,
            SubjectId = enrollment.SubjectId,
        };
        var savedEntity = _uow.Enrollments.Add(domainData);
        await _uow.SaveChangesAsync();
        var returnEntity = _mapper.MapToPublicAdd(savedEntity);

        return CreatedAtAction("GetEnrollment", new { id = returnEntity.Id }, returnEntity);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<App.Public.DTO.v1.Enrollment>> PostEnrollmentTeacher(App.Public.DTO.v1.Enrollment enrollment)
    {
        if (await _uow.Enrollments.CheckForSameData(enrollment, User.GetUserId()))
        {
            return BadRequest("bad");
        }   
        Console.WriteLine("ID: " + User.GetUserId());
        var domainData = new App.Domain.Enrollment()
        {
            IsTeacher = true,
            IsAccepted = true,
            AppUserId = enrollment.AppUserId,
            SemesterId = enrollment.SemesterId,
            SubjectId = enrollment.SubjectId,
        };
        var savedEntity = _uow.Enrollments.Add(domainData);
        await _uow.SaveChangesAsync();
        var returnEntity = _mapper.MapToPublicAdd(savedEntity);

        return CreatedAtAction("GetEnrollment", new { id = returnEntity.Id }, returnEntity);
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Roles = "admin, teacher",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteEnrollment(Guid id)
    {
        var enrollment = await _uow.Enrollments.FirstOrDefaultAsync(id);
        if (enrollment == null)
        {
            return NotFound();
        }

        _uow.Enrollments.Remove(enrollment);
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}