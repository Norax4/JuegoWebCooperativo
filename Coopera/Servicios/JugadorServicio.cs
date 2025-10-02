using Coopera.Datos;

namespace Coopera.Servicios
{
    public class JugadorServicio
    {
        private readonly AppDbContext _context;

        public JugadorServicio(AppDbContext context)
        {
            _context = context;
        }


    }
}
