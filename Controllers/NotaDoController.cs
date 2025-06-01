using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pf_final_ds2.Models;

namespace pf_final_ds2.Controllers
{
    public class NotaDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotaDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var notas = await _context.Notas
                .Include(n => n.Inscripcion)
                    .ThenInclude(i => i.Estudiante)
                .Include(n => n.Inscripcion)
                    .ThenInclude(i => i.Materia)
                .ToListAsync();
            return View(notas);
        }

        public IActionResult Create()
        {
            ViewData["Inscripciones"] = _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nota nota)
        {
            if (ModelState.IsValid)
            {
                nota.NotaFinal = Math.Round((nota.Parcial1 + nota.Parcial2 + nota.ExamenFinal) / 3, 2);
                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Inscripciones"] = _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .ToList();
            return View(nota);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var nota = await _context.Notas
                .Include(n => n.Inscripcion)
                    .ThenInclude(i => i.Estudiante)
                .Include(n => n.Inscripcion)
                    .ThenInclude(i => i.Materia)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (nota == null)
            {
                return NotFound();
            }

            ViewData["Inscripciones"] = _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .ToList();

            return View(nota);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Nota nota)
        {
            if (ModelState.IsValid)
            {
                nota.NotaFinal = Math.Round((nota.Parcial1 + nota.Parcial2 + nota.ExamenFinal) / 3, 2);
                _context.Update(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Inscripciones"] = _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .ToList();
            return View(nota);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var nota = await _context.Notas
                .Include(n => n.Inscripcion)
                    .ThenInclude(i => i.Estudiante)
                .Include(n => n.Inscripcion)
                    .ThenInclude(i => i.Materia)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
