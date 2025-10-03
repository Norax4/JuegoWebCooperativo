using Coopera.DTOs;
using Coopera.Data;
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

        [HttpGet]
        public IActionResult Index()
        {
            int? partidaId = HttpContext.Session.GetInt32("PartidaId");
            int? jugadorId = HttpContext.Session.GetInt32("JugadorId");

            if (!jugadorId.HasValue || !partidaId.HasValue)
                return RedirectToAction("Index", "Home");

            JugadorPartida jugadorPartida = _context.JugadoresPartidas
            .Include(jp => jp.Partida)
            .Include(jp => jp.Jugador)
            .FirstOrDefault(jp => jp.JugadorId == jugadorId && jp.PartidaId == partidaId)!;

            if (jugadorPartida == null)
            {
                return NotFound("Jugador o partida no encontrados.");
            }

            return View(jugadorPartida);
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

        [HttpPost]
        public async Task<IActionResult> EnviarRecursos([FromBody] RecursoRequest request)
        {
            int? partidaId = HttpContext.Session.GetInt32("PartidaId");
            int? jugadorId = HttpContext.Session.GetInt32("JugadorId");

            if (!jugadorId.HasValue || !partidaId.HasValue)
                return RedirectToAction("Index", "Home");

            JugadorPartida? jugadorPartida = await _context.JugadoresPartidas
                .Include(jp => jp.Partida)
                .Include(jp => jp.Jugador)
                .FirstOrDefaultAsync(jp => jp.JugadorId == jugadorId && jp.PartidaId == partidaId);

            if (jugadorPartida == null)
                return NotFound("Jugador o partida no encontrados.");

            jugadorPartida.SumarRecurso(request.Recurso);

            await _context.SaveChangesAsync();

            return Json(new
            {
                totalMadera = jugadorPartida.Partida.Madera,
                totalPiedra = jugadorPartida.Partida.Piedra,
                totalComida = jugadorPartida.Partida.Comida
            });
        }
    }
}