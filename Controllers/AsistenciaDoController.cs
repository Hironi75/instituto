using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pf_final_ds2.Models;

namespace pf_final_ds2.Controllers
{
    public class AsistenciaDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsistenciaDoController(ApplicationDbContext context)
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

        private void CargarInscripcionesYMaterias()
        {
            var inscripciones = _context.Inscripciones.ToList();
            if (inscripciones == null || inscripciones.Count == 0)
            {
                ModelState.AddModelError("Inscripciones", "No hay inscripciones disponibles.");
            }
            ViewData["Inscripciones"] = inscripciones;

            var materias = _context.Materias.ToList();
            if (materias == null || materias.Count == 0)
            {
                ModelState.AddModelError("Materias", "No hay materias disponibles.");
            }
            ViewData["Materias"] = materias;
        }

        public IActionResult Create()
        {
            ViewData["Inscripciones"] = _context.Inscripciones
                .Include(i => i.Estudiante!)
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
                .Include(i => i.Estudiante!)
                .ToList();
            return View(asistencia);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var asistencia = await _context.Asistencias
                .Include(a => a.Inscripcion)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asistencia == null)
            {
                return NotFound();
            }
            CargarInscripcionesYMaterias();
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
            CargarInscripcionesYMaterias();
            return View(asistencia);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var asistencia = await _context.Asistencias
                .Include(a => a.Inscripcion)
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