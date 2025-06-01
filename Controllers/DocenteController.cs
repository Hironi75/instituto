using Microsoft.AspNetCore.Mvc;

namespace pf_final_ds2.Controllers
{
    public class DocenteController : Controller
    {
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
    }
}
