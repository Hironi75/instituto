using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pf_final_ds2.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace pf_final_ds2.Controllers
{
    public class CertificadoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CertificadoController(ApplicationDbContext context)
        {
            _context = context;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<IActionResult> Index()
        {
            var certificados = await _context.Certificados
                .Include(c => c.Estudiante)
                .Include(c => c.Semestre)
                .ToListAsync();
            return View(certificados);
        }

        public IActionResult Create()
        {
            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Semestres"] = _context.Semestres.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Certificado certificado)
        {
            if (ModelState.IsValid)
            {
                certificado.FechaGeneracion = DateTime.Now;
                _context.Add(certificado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Semestres"] = _context.Semestres.ToList();
            return View(certificado);
        }

        public async Task<IActionResult> Print(int id)
        {
            var certificado = await _context.Certificados
                .Include(c => c.Estudiante)
                .Include(c => c.Semestre)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (certificado == null)
            {
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);

                        page.Content().Background(Colors.White).Column(column =>
                        {
                            column.Spacing(20);
                            column.Item().AlignCenter().Text("CERTIFICADO").FontSize(32).Bold();
                            column.Item().AlignCenter().Text($"A: {certificado.Estudiante?.NombreCompleto}").FontSize(20);
                            column.Item().AlignCenter().Text($"Fecha: {DateTime.Now.ToString("dd/MM/yyyy")}").FontSize(16);

                            column.Item().Row(row =>
                            {
                                row.RelativeItem().AlignCenter().Text("_____________________").FontSize(12);
                                row.RelativeItem().AlignCenter().Text("_____________________").FontSize(12);
                                row.RelativeItem().AlignCenter().Text("_____________________").FontSize(12);
                            });

                            column.Item().Row(row =>
                            {
                                row.RelativeItem().AlignCenter().Text("Director de Carrera").FontSize(12);
                                row.RelativeItem().AlignCenter().Text("Rector").FontSize(12);
                                row.RelativeItem().AlignCenter().Text("Docente").FontSize(12);
                            });
                        });
                    });
                }).GeneratePdf(memoryStream);

                return File(memoryStream.ToArray(), "application/pdf", $"Certificado_{certificado.Id}.pdf");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado == null)
            {
                return NotFound();
            }
            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Semestres"] = _context.Semestres.ToList();
            return View(certificado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Certificado certificado)
        {
            if (id != certificado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certificado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Certificados.Any(e => e.Id == id))
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
            ViewData["Estudiantes"] = _context.Estudiantes.ToList();
            ViewData["Semestres"] = _context.Semestres.ToList();
            return View(certificado);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var certificado = await _context.Certificados
                .Include(c => c.Estudiante)
                .Include(c => c.Semestre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificado == null)
            {
                return NotFound();
            }
            return View(certificado);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado != null)
            {
                _context.Certificados.Remove(certificado);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
