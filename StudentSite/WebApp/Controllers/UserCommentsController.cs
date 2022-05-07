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
    public class UserCommentsController : Controller
    {
        private readonly AppDbContext _context;

        public UserCommentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserComments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserComments.Include(u => u.AppUser).Include(u => u.UserPost);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserComments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComments
                .Include(u => u.AppUser)
                .Include(u => u.UserPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userComment == null)
            {
                return NotFound();
            }

            return View(userComment);
        }

        // GET: UserComments/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname");
            ViewData["UserPostId"] = new SelectList(_context.UserPosts, "Id", "Text");
            return View();
        }

        // POST: UserComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentText,UserPostId,AppUserId,CreatedAt,Id")] UserComment userComment)
        {
            if (ModelState.IsValid)
            {
                userComment.Id = Guid.NewGuid();
                _context.Add(userComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userComment.AppUserId);
            ViewData["UserPostId"] = new SelectList(_context.UserPosts, "Id", "Text", userComment.UserPostId);
            return View(userComment);
        }

        // GET: UserComments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComments.FindAsync(id);
            if (userComment == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userComment.AppUserId);
            ViewData["UserPostId"] = new SelectList(_context.UserPosts, "Id", "Text", userComment.UserPostId);
            return View(userComment);
        }

        // POST: UserComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CommentText,UserPostId,AppUserId,CreatedAt,Id")] UserComment userComment)
        {
            if (id != userComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCommentExists(userComment.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userComment.AppUserId);
            ViewData["UserPostId"] = new SelectList(_context.UserPosts, "Id", "Text", userComment.UserPostId);
            return View(userComment);
        }

        // GET: UserComments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComments
                .Include(u => u.AppUser)
                .Include(u => u.UserPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userComment == null)
            {
                return NotFound();
            }

            return View(userComment);
        }

        // POST: UserComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userComment = await _context.UserComments.FindAsync(id);
            _context.UserComments.Remove(userComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCommentExists(Guid id)
        {
            return _context.UserComments.Any(e => e.Id == id);
        }
    }
}
