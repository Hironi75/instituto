using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pf_final_ds2.Models;

namespace pf_final_ds2.Controllers
{
    public class SemestreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SemestreController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var semestres = await _context.Semestres.ToListAsync();
            return View(semestres);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Semestre semestre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(semestre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(semestre);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var semestre = await _context.Semestres.FindAsync(id);
            if (semestre == null)
            {
                return NotFound();
            }
            return View(semestre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Semestre semestre)
        {
            if (ModelState.IsValid)
            {
                _context.Update(semestre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(semestre);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var semestre = await _context.Semestres.FindAsync(id);
            if (semestre == null)
            {
                return NotFound();
            }
            return View(semestre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var semestre = await _context.Semestres.FindAsync(id);
            if (semestre != null)
            {
                _context.Semestres.Remove(semestre);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
