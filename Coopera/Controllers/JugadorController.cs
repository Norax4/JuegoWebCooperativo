using Coopera.Datos;
using Coopera.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coopera.Controllers
{
    public class JugadorController : Controller
    {
        private readonly AppDbContext _context;

        public JugadorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult PantallaParcialJugador()
        {
            return PartialView("_RegistroJugador");
        }

        [HttpPost]
        public ActionResult RegistrarJugador(string nombre)
        {
            if (nombre == null)
            {
                Console.WriteLine("No se introdujo ningun nombre");
                return Json(new { success = false, message = "Nombre inválido" });
            }

            Jugador jugadorExistente = _context.Jugadores.FirstOrDefault(j => j.Nombre == nombre);

            if (jugadorExistente != null)
            {
                Console.WriteLine("Ya hay un jugador con ese nombre");
                return Json(new { success = false, message = "Nombre inválido" });
            }

            Jugador nuevoJugador = new Jugador
            {
                Nombre = nombre
            };

            _context.Jugadores.Add(nuevoJugador);
            _context.SaveChanges();
            Console.WriteLine("Jugador registrado con exito");

            return Json(new { success = true, message = "Jugador registrado correctamente" });
        }
    }
}
