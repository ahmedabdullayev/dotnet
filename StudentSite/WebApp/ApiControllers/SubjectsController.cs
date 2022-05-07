#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // TODO USE BLL AND DTO FRO REST
    public class SubjectsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public SubjectsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectsDTO>>> GetSubjects()
        {
            var res = (await _uow.Subjects.GetAllAsync())
                .Select(x => new SubjectsDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                }).ToList();
            return res;
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(Guid id)
        {
            var subject = await _uow.Subjects.FirstOrDefaultAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return null;
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(Guid id, SubjectsDTO subject)
        {
            if (id != subject.Id)
            {
                return BadRequest();
            }

            var subjectFromDb = await _uow.Subjects.FirstOrDefaultAsync(id);
            if (subjectFromDb == null)
            {
                return NotFound();
            }
            
            subjectFromDb.Name.SetTranslation(subject.Name);
            subjectFromDb.Description.SetTranslation(subject.Description);
            // _uow.Subjects.Entry(subjectFromDb).State = EntityState.Modified;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SubjectExists(subject.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(SubjectsDTO subject)
        {
            var subj = new Subject();
            subj.Name.SetTranslation(subject.Name);
            subj.Description.SetTranslation(subject.Description);
            // _uow.Subjects.Add(subj);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetSubject", new { id = subj.Id }, subj);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
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

        private async Task<bool> SubjectExists(Guid id)
        {
            return await _uow.Subjects.ExistsAsync(id);
        }
    }
}
