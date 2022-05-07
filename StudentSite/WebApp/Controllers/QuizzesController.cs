#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Base.Extensions;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly IAppBLL _bll;
        public QuizzesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index()
        {
            var res = await _bll.Quizzes.GetAllAsync(User.GetUserId());
            return View(res);
        }

        // GET: Quizzes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _bll.Quizzes.FirstOrDefaultAsync(id.Value);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quizzes/Create
        public async Task<IActionResult> Create()
        {
            // ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Description");
            var quizVm = new QuizzesCreateEditVM();
            quizVm.SubjectSelectList = new SelectList(await _bll.Subjects.GetAllAsync(), nameof(Subject.Id), nameof(Subject.Name));
            return View(quizVm);
        }

        // POST: Quizzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizzesCreateEditVM quizVm)
        {
            if (ModelState.IsValid)
            {
                quizVm.Quiz.AppUserId = User.GetUserId();
                quizVm.Quiz.Id = Guid.NewGuid();
                _bll.Quizzes.Add(quizVm.Quiz);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Firstname", quizVm.Quiz.AppUserId);
            quizVm.SubjectSelectList = new SelectList(await _bll.Subjects.GetAllAsync(), nameof(Subject.Id), nameof(Subject.Name), quizVm.Quiz.SubjectId);
            return View(quizVm);
        }

        // GET: Quizzes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _bll.Quizzes.FirstOrDefaultAsync(id.Value);
            
            if (quiz == null)
            {
                return NotFound();
            }
            var quizVm = new QuizzesCreateEditVM();
            quizVm.Quiz = quiz;
            
            quizVm.SubjectSelectList = new SelectList(await _bll.Subjects.GetAllAsync(), nameof(Subject.Id), nameof(Subject.Name), quizVm.Quiz.SubjectId);
            return View(quizVm);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, QuizzesCreateEditVM quizVm)
        {
            if (id != quizVm.Quiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    quizVm.Quiz.AppUserId = User.GetUserId();
                    _bll.Quizzes.Update(quizVm.Quiz);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await QuizExists(quizVm.Quiz.Id))
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
            quizVm.SubjectSelectList = new SelectList(await _bll.Subjects.GetAllAsync(), nameof(Subject.Id), nameof(Subject.Name), quizVm.Quiz.SubjectId);
            return View(quizVm);
        }

        // GET: Quizzes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _bll.Quizzes.FirstOrDefaultAsync(id.Value);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Quizzes.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> QuizExists(Guid id)
        {
            return await _bll.Quizzes.ExistsAsync(id);
        }
    }
}
