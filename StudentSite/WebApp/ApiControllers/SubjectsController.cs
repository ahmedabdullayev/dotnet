#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectsDTO>>> GetSubjects()
        {
            var res = (await _context.Subjects.ToListAsync())
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
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
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

            var subjectFromDb = await _context.Subjects.FindAsync(id);
            if (subjectFromDb == null)
            {
                return NotFound();
            }
            
            subjectFromDb.Name.SetTranslation(subject.Name);
            subjectFromDb.Description.SetTranslation(subject.Description);
            _context.Entry(subjectFromDb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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
            _context.Subjects.Add(subj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubject", new { id = subj.Id }, subj);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectExists(Guid id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}
