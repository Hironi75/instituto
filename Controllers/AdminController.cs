using Microsoft.AspNetCore.Mvc;

namespace pf_final_ds2.Controllers
{
    public class AdminController : Controller
    {
        private readonly pf_final_ds2.Models.ApplicationDbContext _context;

        public AdminController(pf_final_ds2.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Aquí puedes agregar lógica para cerrar la sesión del usuario
            return RedirectToAction("Login", "Usuario");
        }

        public IActionResult Reportes()
        {
            int totalUsuarios = _context.Usuarios.Count();
            int totalEstudiantes = _context.Estudiantes.Count();
            int totalMaterias = _context.Materias.Count();
            int totalInscripciones = _context.Inscripciones.Count();
            int totalNotas = _context.Notas.Count();
            int totalAsistencias = _context.Asistencias.Count();

            ViewData["TotalUsuarios"] = totalUsuarios;
            ViewData["TotalEstudiantes"] = totalEstudiantes;
            ViewData["TotalMaterias"] = totalMaterias;
            ViewData["TotalInscripciones"] = totalInscripciones;
            ViewData["TotalNotas"] = totalNotas;
            ViewData["TotalAsistencias"] = totalAsistencias;

            ViewData["DashboardPieLabels"] = new string[] { "Usuarios", "Estudiantes", "Materias", "Inscripciones", "Notas", "Asistencias" };
            ViewData["DashboardPieData"] = new int[] {
                totalUsuarios,
                totalEstudiantes,
                totalMaterias,
                totalInscripciones,
                totalNotas,
                totalAsistencias
            };
            return View();
        }
    }
}
