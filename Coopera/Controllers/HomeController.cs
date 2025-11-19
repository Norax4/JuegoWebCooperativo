using Coopera.Data;
using Coopera.Models;
using Coopera.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Coopera.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPartidaService _partidaService;

        public HomeController(IPartidaService partidaService)
        {
            _partidaService = partidaService;
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
            try
            {
                await _partidaService.NuevaPartida(dificultad, nombreJugador, HttpContext.Session);
                return RedirectToAction("Index", "Partida");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}