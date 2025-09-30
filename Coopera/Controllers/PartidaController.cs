using Coopera.Datos;
using Coopera.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coopera.Controllers
{
    public class PartidaController : Controller
    {
        private readonly AppDbContext _context;

        public PartidaController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult PantallaParcialPartida()
        {
            return PartialView("_DificultadPartida");
        }

        [HttpPost]
        public IActionResult CrearPartida(string dificultad /*int jugadorId*/)
        {
            int jugadorId = 1; // ID del jugador que crea la partida (debería obtenerse del contexto de usuario autenticado)

            Partida nuevaPartida = new Partida
            {
                Dificultad = dificultad switch
                {
                    "Facil" => Dificultad.Facil,
                    "Media" => Dificultad.Media,
                    "Dificil" => Dificultad.Dificil,
                    _ => Dificultad.Facil
                }
            };

            _context.Partidas.Add(nuevaPartida);

            JugadorPartida jugadorPartida = new JugadorPartida
            {
                JugadorId = jugadorId,
                Partida = nuevaPartida
            };

            _context.JugadoresPartidas.Add(jugadorPartida);
            _context.SaveChanges();

            return RedirectToAction("PantallaPrincipal", new { id = nuevaPartida.Id });
        }

        [HttpGet]
        public IActionResult PantallaPrincipal(int id)
        {
            Partida partida = _context.Partidas
                .Include(p => p.PartidaJugadores)
                .FirstOrDefault(p => p.Id == id);

            if (partida == null)
            {
                return NotFound();
            }

            return View(partida);
        }

        [HttpGet]
        public ActionResult MinijuegoComida()
        {
            return PartialView("_MinijuegoComida");
        }

        [HttpGet]
        public ActionResult MinijuegoMadera()
        {
            return PartialView("_MinijuegoMadera");
        }

        [HttpGet]
        public ActionResult MinijuegoPiedra()
        {
            return PartialView("_MinijuegoPiedra");
        }
    }
}
