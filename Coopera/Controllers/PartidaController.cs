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
        public IActionResult CrearMinijuego(string value)
        {
            int? partidaId = HttpContext.Session.GetInt32("PartidaId");
            int? jugadorId = HttpContext.Session.GetInt32("JugadorId");

            if (!jugadorId.HasValue || !partidaId.HasValue)
                return RedirectToAction("Index", "Home");

            int[] numberArray = _partidaService.crearArrayMinijuegos(value);
            if (value == "Madera")
            {
                return Json(new
                {
                    arrayNumeros = numberArray
                });
            }

            string question = _partidaService.GenerarPreguntaAleatoria(value);
            return Json(new
            {
                arrayNumeros = numberArray,
                pregunta = question
            });
        }

        [HttpPost]
        public async Task<IActionResult> EnviarRecursos([FromBody] RecursoRequest request)
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

                return Json(new
                {
                    totalMadera = jugadorPartida.Partida!.Madera,
                    totalPiedra = jugadorPartida.Partida.Piedra,
                    totalComida = jugadorPartida.Partida.Comida,
                    partidaFinalizada
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