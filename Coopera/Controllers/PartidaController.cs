using Coopera.DTOs;
using Coopera.Data;
using Coopera.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Coopera.Services;
using Coopera.Factories.Minijuegos;

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
        public async Task<IActionResult> CrearMinijuego([FromBody] RecursoRequest request)
        {
            int? partidaId = HttpContext.Session.GetInt32("PartidaId");
            int? jugadorId = HttpContext.Session.GetInt32("JugadorId");

            if (!jugadorId.HasValue || !partidaId.HasValue)
                return RedirectToAction("Index", "Home");

            try
            {
                IMinijuego minijuego = await _partidaService.CrearMiniJuego(partidaId, jugadorId, request.Recurso);

                switch (minijuego) {
                    case MinijuegoMadera madera:
                        return Json(new
                        {
                            pregunta = madera.Pregunta,
                            respuestaCorrecta = madera.RespuestaCorrecta
                        });
                    case MinijuegoPiedra piedra:
                        return Json(new
                        {
                            pregunta = piedra.Pregunta,
                            respuestaCorrecta = piedra.RespuestaCorrecta,
                            secuencia = piedra.SecuenciaNumerica
                        });
                    case MinijuegoComida comida:
                        return Json(new
                        {
                            pregunta = comida.Pregunta,
                            respuestaCorrecta = comida.RespuestaCorrecta,
                            secuencia = comida.SecuenciaNumerica
                        });
                    default:
                        return BadRequest("Tipo de minijuego no reconocido.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index", "Home");
            }
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