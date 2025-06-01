using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pf_final_ds2.Models;

namespace pf_final_ds2.Controllers
{
    public class AsignacionDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsignacionDoController(ApplicationDbContext context)
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

        private void CargarDocentesYMaterias()
        {
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
        }

        public IActionResult Create()
        {
            CargarDocentesYMaterias();
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
            CargarDocentesYMaterias();
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
            CargarDocentesYMaterias();
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
            CargarDocentesYMaterias();
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
