using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pf_final_ds2.Models;

namespace pf_final_ds2.Controllers
{
    public class AsignacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsignacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var asignaciones = await _context.Asignaciones
                .Include(a => a.Docente)
                .Include(a => a.Materia)
                .ToListAsync();
            return View(asignaciones);
        }

        public IActionResult Create()
        {
            ViewData["Docentes"] = _context.Usuarios.Where(u => u.Tipo == "Docente").ToList();
            ViewData["Materias"] = _context.Materias.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Asignacion asignacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var docentes = _context.Usuarios.Where(u => u.Tipo == "Docente").ToList();
            if (docentes == null || docentes.Count == 0)
            {
                ModelState.AddModelError("Docentes", "No hay docentes disponibles.");
            }
            ViewData["Docentes"] = docentes;

            var materias = _context.Materias.ToList();
            if (materias == null || materias.Count == 0)
            {
                ModelState.AddModelError("Materias", "No hay materias disponibles.");
            }
            ViewData["Materias"] = materias;
            return View(asignacion);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var asignacion = await _context.Asignaciones
                .Include(a => a.Docente)
                .Include(a => a.Materia)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asignacion == null)
            {
                return NotFound();
            }

            var docentes = _context.Usuarios.Where(u => u.Tipo == "Docente").ToList();
            if (docentes == null || docentes.Count == 0)
            {
                ModelState.AddModelError("Docentes", "No hay docentes disponibles.");
            }
            ViewData["Docentes"] = docentes;

            var materias = _context.Materias.ToList();
            if (materias == null || materias.Count == 0)
            {
                ModelState.AddModelError("Materias", "No hay materias disponibles.");
            }
            ViewData["Materias"] = materias;

            return View(asignacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Asignacion asignacion)
        {
            if (ModelState.IsValid)
            {
                _context.Update(asignacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var docentes = _context.Usuarios.Where(u => u.Tipo == "Docente").ToList();
            if (docentes == null || docentes.Count == 0)
            {
                ModelState.AddModelError("Docentes", "No hay docentes disponibles.");
            }
            ViewData["Docentes"] = docentes;

            var materias = _context.Materias.ToList();
            if (materias == null || materias.Count == 0)
            {
                ModelState.AddModelError("Materias", "No hay materias disponibles.");
            }
            ViewData["Materias"] = materias;

            return View(asignacion);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var asignacion = await _context.Asignaciones
                .Include(a => a.Docente)
                .Include(a => a.Materia)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asignacion == null)
            {
                return NotFound();
            }

            return View(asignacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignacion = await _context.Asignaciones.FindAsync(id);
            if (asignacion != null)
            {
                _context.Asignaciones.Remove(asignacion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
