using Coopera.Data;
using Microsoft.AspNetCore.Mvc;

namespace Coopera.Controllers
{
    public class PartidaController : Controller
    {
        private readonly AppDbContext _context;

        public PartidaController(AppDbContext context)
        { _context = context; }

        public IActionResult Index()
        { return View(); }
    }
}
