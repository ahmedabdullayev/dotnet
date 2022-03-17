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
    public class UserChoicesController : Controller
    {
        private readonly AppDbContext _context;

        public UserChoicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserChoices
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserChoices.Include(u => u.Answer).Include(u => u.AppUser).Include(u => u.Question).Include(u => u.Quiz).Include(u => u.UserQuiz);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserChoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userChoice = await _context.UserChoices
                .Include(u => u.Answer)
                .Include(u => u.AppUser)
                .Include(u => u.Question)
                .Include(u => u.Quiz)
                .Include(u => u.UserQuiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userChoice == null)
            {
                return NotFound();
            }

            return View(userChoice);
        }

        // GET: UserChoices/Create
        public IActionResult Create()
        {
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "AnswerText");
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "QuestionText");
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Description");
            ViewData["UserQuizId"] = new SelectList(_context.UserQuizzes, "Id", "Id");
            return View();
        }

        // POST: UserChoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizId,QuestionId,AnswerId,UserQuizId,AppUserId,CreatedAt,Id")] UserChoice userChoice)
        {
            if (ModelState.IsValid)
            {
                userChoice.Id = Guid.NewGuid();
                _context.Add(userChoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "AnswerText", userChoice.AnswerId);
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userChoice.AppUserId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "QuestionText", userChoice.QuestionId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Description", userChoice.QuizId);
            ViewData["UserQuizId"] = new SelectList(_context.UserQuizzes, "Id", "Id", userChoice.UserQuizId);
            return View(userChoice);
        }

        // GET: UserChoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userChoice = await _context.UserChoices.FindAsync(id);
            if (userChoice == null)
            {
                return NotFound();
            }
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "AnswerText", userChoice.AnswerId);
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userChoice.AppUserId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "QuestionText", userChoice.QuestionId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Description", userChoice.QuizId);
            ViewData["UserQuizId"] = new SelectList(_context.UserQuizzes, "Id", "Id", userChoice.UserQuizId);
            return View(userChoice);
        }

        // POST: UserChoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("QuizId,QuestionId,AnswerId,UserQuizId,AppUserId,CreatedAt,Id")] UserChoice userChoice)
        {
            if (id != userChoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userChoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserChoiceExists(userChoice.Id))
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
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "AnswerText", userChoice.AnswerId);
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userChoice.AppUserId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "QuestionText", userChoice.QuestionId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Description", userChoice.QuizId);
            ViewData["UserQuizId"] = new SelectList(_context.UserQuizzes, "Id", "Id", userChoice.UserQuizId);
            return View(userChoice);
        }

        // GET: UserChoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userChoice = await _context.UserChoices
                .Include(u => u.Answer)
                .Include(u => u.AppUser)
                .Include(u => u.Question)
                .Include(u => u.Quiz)
                .Include(u => u.UserQuiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userChoice == null)
            {
                return NotFound();
            }

            return View(userChoice);
        }

        // POST: UserChoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userChoice = await _context.UserChoices.FindAsync(id);
            _context.UserChoices.Remove(userChoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserChoiceExists(Guid id)
        {
            return _context.UserChoices.Any(e => e.Id == id);
        }
    }
}
