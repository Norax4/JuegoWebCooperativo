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
            return PartialView("_RegistroUsuario");
        }

        [HttpPost]
        public IActionResult RegistrarJugador(string nombre)
        {
            if (nombre == null)
            {
                Console.WriteLine("No se introdujo ningun nombre");
                return View("Index", "Home");
            }

            Jugador jugadorExistente = _context.Jugadores.FirstOrDefault(j => j.Nombre == nombre);

            if (jugadorExistente != null)
            {
                Console.WriteLine("Ya hay un jugador con ese nombre");
                return View("Index", "Home");
            }

            Jugador nuevoJugador = new Jugador
            {
                Nombre = nombre
            };

            _context.Jugadores.Add(nuevoJugador);
            _context.SaveChanges();

            return View("Index", "Home");
        }
    }
}
