using Coopera.DTOs;
using Coopera.Data;
using Coopera.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Coopera.Services;

namespace Coopera.Controllers
{
    public class PartidaController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PartidaService _partidaService;

        public PartidaController(AppDbContext context, PartidaService partidaService)
        {
            _context = context;
            _partidaService = partidaService;
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

        [HttpPost]
        public async Task<IActionResult> NuevaPartida(Dificultad dificultad)
        {
            HttpContext.Session.Remove("PartidaId");
            int? jugadorId = HttpContext.Session.GetInt32("JugadorId");

            if (!jugadorId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            Jugador? jugador = await _context.Jugadores.
            FirstOrDefaultAsync(j => j.Id == jugadorId);

            if (jugador == null)
            {
                return NotFound("Jugador no encontrado.");
            }

            try
            {
                await _partidaService.NuevaPartida(dificultad, jugador.Nombre, HttpContext.Session);
                return RedirectToAction("Index", "Partida");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }



        [HttpPost]
        public IActionResult CrearMinijuego([FromBody] RecursoRequestDto request)
        {
            Console.WriteLine("Entra a CM:" + request.Recurso);
            int? partidaId = HttpContext.Session.GetInt32("PartidaId");
            int? jugadorId = HttpContext.Session.GetInt32("JugadorId");

            if (!jugadorId.HasValue || !partidaId.HasValue)
                return RedirectToAction("Index", "Home");

            Console.WriteLine("Valor recibido: " + request.Recurso);
            int[] numberArray = _partidaService.crearArrayMinijuegos(request.Recurso);
            if (request.Recurso == Recurso.Madera)
            {
                return Json(new
                {
                    arrayNumeros = numberArray
                });
            }

            string question = _partidaService.GenerarPreguntaAleatoria(request.Recurso);
            return Json(new
            {
                arrayNumeros = numberArray,
                pregunta = question
            });
        }

        [HttpPost]
        public async Task<IActionResult> EnviarRecursos([FromBody] RecursoRequestDto request)
        {
            int? partidaId = HttpContext.Session.GetInt32("PartidaId");
            int? jugadorId = HttpContext.Session.GetInt32("JugadorId");

            if (!jugadorId.HasValue || !partidaId.HasValue)
                return RedirectToAction("Index", "Home");

            try
            {
                JugadorPartida jugadorPartida = await _partidaService.AumentarRecurso(partidaId, jugadorId, request.Recurso);


                Partida partida = jugadorPartida.Partida!;

                bool partidaFinalizada = partida.PartidaFinalizada;

                List<JugadorPartida> jugadoresPartida = _context.JugadoresPartidas
               .Include(jp => jp.Jugador)
               .Where(jp => jp.PartidaId == partidaId)
               .ToList();

                List<RecursosPorJugadorDto> jugadoresStats = jugadoresPartida.Select(jp => new RecursosPorJugadorDto
                {
                    NombreJugador = jp.Jugador?.Nombre ?? $"Jugador {jp.JugadorId}",
                    Madera = jp.MaderaJugador,
                    Piedra = jp.PiedraJugador,
                    Comida = jp.ComidaJugador
                }).ToList();

                return Json(new
                {
                    totalMadera = jugadorPartida.Partida!.Madera,
                    totalPiedra = jugadorPartida.Partida.Piedra,
                    totalComida = jugadorPartida.Partida.Comida,
                    partidaFinalizada,
                    jugadoresStats
                });
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}