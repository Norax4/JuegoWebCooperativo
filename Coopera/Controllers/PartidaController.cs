using Coopera.Datos;
using Coopera.Models;
using Coopera.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coopera.Controllers
{
    public class PartidaController : Controller
    {
        private readonly PartidaServicio _partidaServ;

        public PartidaController(PartidaServicio context)
        {
            _partidaServ = context;
        }

        [HttpGet]
        public ActionResult PantallaParcialPartida()
        {
            return PartialView("_DificultadPartida");
        }

        [HttpPost]
        public ActionResult CrearPartida(string dificultad, string jugadorNombre)
        {
            Partida? nuevaPartida = _partidaServ.CrearPartida(dificultad, jugadorNombre);

            Console.WriteLine("Verificar partida");
            if (nuevaPartida == null)
            {
                Console.WriteLine("No se pudo crear la partida");
                return BadRequest("No se pudo crear la partida. No se encontró un jugador.");
            }

            TempData["PlayerName"] = jugadorNombre;
            return Json(new { success = true, partidaId = nuevaPartida.Id });
        }

        [HttpGet]
        public IActionResult PantallaPrincipal(int id)
        {
            Partida? partida = _partidaServ.ObtenerPartidaPorId(id);

            if (partida == null)
            {
                Console.WriteLine("No se encontro la partida");
                return NotFound("Partida no encontrada");
            }
            Console.WriteLine("Partida encontrada: " + partida.Id);
            ViewBag.PlayerName = TempData["PlayerName"] as string ?? "Jugador Desconocido";
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
