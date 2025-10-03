using Coopera.Data;
using Coopera.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Coopera.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("PartidaId") != null)
            {
                return RedirectToAction("Index", "Partida");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ComenzarPartida(string nombreJugador, Dificultad dificultad)
        {
            //Chequear si existe partida en curso
            if (HttpContext.Session.GetInt32("PartidaId") != null)
                return BadRequest("Ya hay una partida en curso.");

            Jugador jugador = new(nombreJugador);
            Partida partida = new Partida(dificultad);

            _context.Jugadores.Add(jugador);
            _context.Partidas.Add(partida);

            await _context.SaveChangesAsync();

            JugadorPartida jugadorPartida = new(jugador.Id, partida.Id);

            _context.JugadoresPartidas.Add(jugadorPartida);

            await _context.SaveChangesAsync();

            // Guardar Id de partida y jugador en sesion
            HttpContext.Session.SetInt32("PartidaId", partida.Id);
            HttpContext.Session.SetInt32("JugadorId", jugador.Id);

            return RedirectToAction("Index", "Partida");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}