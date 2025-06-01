using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pf_final_ds2.Models;

namespace pf_final_ds2.Controllers
{
    public class AsistenciaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsistenciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var asistencias = await _context.Asistencias
                .Include(a => a.Inscripcion)
                .ThenInclude(i => i.Estudiante)
                .Include(a => a.Inscripcion)
                .ThenInclude(i => i.Materia)
                .ToListAsync();
            return View(asistencias);
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
        public async Task<IActionResult> Create(Asistencia asistencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Inscripciones"] = _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .ToList();
            return View(asistencia);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var asistencia = await _context.Asistencias
                .Include(a => a.Inscripcion)
                .ThenInclude(i => i.Estudiante)
                .Include(a => a.Inscripcion)
                .ThenInclude(i => i.Materia)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asistencia == null)
            {
                return NotFound();
            }

            ViewData["Inscripciones"] = _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .ToList();

            return View(asistencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Asistencia asistencia)
        {
            if (ModelState.IsValid)
            {
                _context.Update(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Inscripciones"] = _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .ToList();

            return View(asistencia);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var asistencia = await _context.Asistencias
                .Include(a => a.Inscripcion)
                .ThenInclude(i => i.Estudiante)
                .Include(a => a.Inscripcion)
                .ThenInclude(i => i.Materia)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asistencia == null)
            {
                return NotFound();
            }

            return View(asistencia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia != null)
            {
                _context.Asistencias.Remove(asistencia);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
