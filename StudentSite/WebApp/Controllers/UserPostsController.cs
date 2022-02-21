#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Posts;

namespace WebApp.Controllers
{
    public class UserPostsController : Controller
    {
        private readonly AppDbContext _context;

        public UserPostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserPosts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserPosts.Include(u => u.AppUser).Include(u => u.Topic);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserPosts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPost = await _context.UserPosts
                .Include(u => u.AppUser)
                .Include(u => u.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPost == null)
            {
                return NotFound();
            }

            return View(userPost);
        }

        // GET: UserPosts/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname");
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Description");
            return View();
        }

        // POST: UserPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Text,TopicId,AppUserId,CreatedAt,Id")] UserPost userPost)
        {
            if (ModelState.IsValid)
            {
                userPost.Id = Guid.NewGuid();
                _context.Add(userPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userPost.AppUserId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Description", userPost.TopicId);
            return View(userPost);
        }

        // GET: UserPosts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPost = await _context.UserPosts.FindAsync(id);
            if (userPost == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userPost.AppUserId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Description", userPost.TopicId);
            return View(userPost);
        }

        // POST: UserPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Text,TopicId,AppUserId,CreatedAt,Id")] UserPost userPost)
        {
            if (id != userPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPostExists(userPost.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", userPost.AppUserId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Description", userPost.TopicId);
            return View(userPost);
        }

        // GET: UserPosts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPost = await _context.UserPosts
                .Include(u => u.AppUser)
                .Include(u => u.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPost == null)
            {
                return NotFound();
            }

            return View(userPost);
        }

        // POST: UserPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userPost = await _context.UserPosts.FindAsync(id);
            _context.UserPosts.Remove(userPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPostExists(Guid id)
        {
            return _context.UserPosts.Any(e => e.Id == id);
        }
    }
}
