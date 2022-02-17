#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class MeetingOptionsController : Controller
    {
        private readonly AppDbContext _context;

        public MeetingOptionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MeetingOptions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MeetingOptions.Include(m => m.Meeting);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MeetingOptions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingOption = await _context.MeetingOptions
                .Include(m => m.Meeting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetingOption == null)
            {
                return NotFound();
            }

            return View(meetingOption);
        }

        // GET: MeetingOptions/Create
        public async Task<IActionResult> Create()
        {
            var vm = new MeetingOptionCreateEditVM();
            vm.MeetingSelectList = new SelectList(
                await _context.Meetings
                    .OrderBy(m => m.AvailableFrom)
                    .Select(x => new {x.Id, x.Title})
                    .ToListAsync(),
                nameof(Meeting.Id),
                nameof(Meeting.Title));
            return View(vm);
        }

        // POST: MeetingOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetingOptionCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vm.MeetingOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description", meetingOption.MeetingId);
            vm.MeetingSelectList = new SelectList(
                await _context.Meetings
                    .OrderBy(m => m.AvailableFrom)
                    .Select(x => new {x.Id, x.Title})
                    .ToListAsync(),
                nameof(Meeting.Id),
                nameof(Meeting.Title),
                vm.MeetingOption.MeetingId);
            return View(vm);
        }

        // GET: MeetingOptions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingOption = await _context.MeetingOptions.FindAsync(id);
            if (meetingOption == null)
            {
                return NotFound();
            }
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description", meetingOption.MeetingId);
            return View(meetingOption);
        }

        // POST: MeetingOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,MeetingId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] MeetingOption meetingOption)
        {
            if (id != meetingOption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetingOption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingOptionExists(meetingOption.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Description", meetingOption.MeetingId);
            return View(meetingOption);
        }

        // GET: MeetingOptions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingOption = await _context.MeetingOptions
                .Include(m => m.Meeting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetingOption == null)
            {
                return NotFound();
            }

            return View(meetingOption);
        }

        // POST: MeetingOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var meetingOption = await _context.MeetingOptions.FindAsync(id);
            _context.MeetingOptions.Remove(meetingOption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingOptionExists(Guid id)
        {
            return _context.MeetingOptions.Any(e => e.Id == id);
        }
    }
}
