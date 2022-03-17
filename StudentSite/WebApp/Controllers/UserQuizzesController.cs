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

namespace WebApp.Controllers
{
    public class UserQuizzesController : Controller
    {
        private readonly AppDbContext _context;

        public UserQuizzesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserQuizzes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserQuizzes.Include(u => u.AppUser).Include(u => u.Quiz);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserQuizzes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userQuiz = await _context.UserQuizzes
                .Include(u => u.AppUser)
                .Include(u => u.Quiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userQuiz == null)
            {
                return NotFound();
            }

            return View(userQuiz);
        }

        // GET: UserQuizzes/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Description");
            return View();
        }

        // POST: UserQuizzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizId,AppUserId,CreatedAt,Id")] UserQuiz userQuiz)
        {
            if (ModelState.IsValid)
            {
                userQuiz.Id = Guid.NewGuid();
                _context.Add(userQuiz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userQuiz.AppUserId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Description", userQuiz.QuizId);
            return View(userQuiz);
        }

        // GET: UserQuizzes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userQuiz = await _context.UserQuizzes.FindAsync(id);
            if (userQuiz == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userQuiz.AppUserId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Description", userQuiz.QuizId);
            return View(userQuiz);
        }

        // POST: UserQuizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("QuizId,AppUserId,CreatedAt,Id")] UserQuiz userQuiz)
        {
            if (id != userQuiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userQuiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserQuizExists(userQuiz.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userQuiz.AppUserId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Description", userQuiz.QuizId);
            return View(userQuiz);
        }

        // GET: UserQuizzes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userQuiz = await _context.UserQuizzes
                .Include(u => u.AppUser)
                .Include(u => u.Quiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userQuiz == null)
            {
                return NotFound();
            }

            return View(userQuiz);
        }

        // POST: UserQuizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userQuiz = await _context.UserQuizzes.FindAsync(id);
            _context.UserQuizzes.Remove(userQuiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserQuizExists(Guid id)
        {
            return _context.UserQuizzes.Any(e => e.Id == id);
        }
    }
}
