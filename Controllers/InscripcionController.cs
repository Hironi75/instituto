using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pf_final_ds2.Models;

namespace pf_final_ds2.Controllers
{
    public class InscripcionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InscripcionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var inscripciones = await _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .Include(i => i.Semestre)
                .ToListAsync();
            return View(inscripciones);
        }

        public IActionResult Create()
        {
            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Materias"] = _context.Materias.ToList();
            ViewData["Semestres"] = _context.Semestres.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inscripcion inscripcion)
        {
            if (ModelState.IsValid)
            {
                if (_context.Inscripciones.Any(i => i.EstudianteId == inscripcion.EstudianteId && i.MateriaId == inscripcion.MateriaId && i.SemestreId == inscripcion.SemestreId))
                {
                    ModelState.AddModelError(string.Empty, "Ya existe una inscripción con estos datos.");
                    ViewData["Estudiantes"] = _context.Estudiantes.ToList();
                    ViewData["Materias"] = _context.Materias.ToList();
                    ViewData["Semestres"] = _context.Semestres.ToList();
                    return View(inscripcion);
                }

                _context.Add(inscripcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Materias"] = _context.Materias.ToList();
            ViewData["Semestres"] = _context.Semestres.ToList();
            return View(inscripcion);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var inscripcion = await _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inscripcion == null)
            {
                return NotFound();
            }

            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Materias"] = _context.Materias.ToList();

            return View(inscripcion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Inscripcion inscripcion)
        {
            if (ModelState.IsValid)
            {
                _context.Update(inscripcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Materias"] = _context.Materias.ToList();

            return View(inscripcion);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var inscripcion = await _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inscripcion == null)
            {
                return NotFound();
            }

            return View(inscripcion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion != null)
            {
                _context.Inscripciones.Remove(inscripcion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
