using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitalTrack.Data;
using VitalTrack.Models;

namespace VitalTrack.Controllers
{
    public class HomeController : Controller
    {
        private readonly VitalTrackContext _context;

        public HomeController(VitalTrackContext context)
        {
            _context = context;
        }

        public IActionResult Index() { return View(); }

        public async Task<IActionResult> Panel()
        {
            var ultimoRegistro = await _context.Registros.OrderByDescending(r => r.Fecha).FirstOrDefaultAsync();
            return View(ultimoRegistro);
        }

        public IActionResult Registrar(int? id)
        {
            if (id != null)
            {
                var registro = _context.Registros.Find(id);
                return View(registro);
            }
            return View(new RegistroSalud());
        }

        [HttpPost]
        public async Task<IActionResult> GuardarRegistro(RegistroSalud modelo)
        {
            ModelState.Remove("Fecha");

            if (ModelState.IsValid)
            {
                if (modelo.Id == 0)
                {
                    modelo.Fecha = DateTime.Now;
                    _context.Registros.Add(modelo);
                }
                else
                {
                    var registroExistente = await _context.Registros.FindAsync(modelo.Id);
                    if (registroExistente != null)
                    {
                        registroExistente.Temperatura = modelo.Temperatura;
                        registroExistente.PresionSistolica = modelo.PresionSistolica;
                        registroExistente.PresionDiastolica = modelo.PresionDiastolica;
                        registroExistente.Agua = modelo.Agua;
                        registroExistente.Sueno = modelo.Sueno;
                        registroExistente.Actividad = modelo.Actividad;
                        _context.Registros.Update(registroExistente);
                    }
                }

                await _context.SaveChangesAsync();

                TempData["MensajeExito"] = "✓ Datos guardados correctamente.";
                return RedirectToAction("Registrar");
            }
            return View("Registrar", modelo);
        }

        public async Task<IActionResult> Historial(DateTime? desde, DateTime? hasta)
        {
            var query = _context.Registros.AsQueryable();

            if (desde.HasValue) query = query.Where(r => r.Fecha >= desde.Value);
            if (hasta.HasValue) query = query.Where(r => r.Fecha <= hasta.Value);

            var registros = await query.OrderByDescending(r => r.Fecha).ToListAsync();

            return View(registros ?? new List<RegistroSalud>());
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var registro = await _context.Registros.FindAsync(id);
            if (registro != null)
            {
                _context.Registros.Remove(registro);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Historial");
        }

        public IActionResult Recomendaciones() { return View(); }
        public IActionResult Acerca() { return View(); }
    }
}