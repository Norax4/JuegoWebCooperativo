using Coopera.Datos;
using Coopera.Models;
using Microsoft.EntityFrameworkCore;

namespace Coopera.Servicios
{
    public class PartidaServicio
    {
        private readonly AppDbContext _context;

        public PartidaServicio(AppDbContext context)
        {
            _context = context;
        }

        // Métodos para manejar la lógica de negocio relacionada con Partida
        public Partida? CrearPartida(string dificultad, string jugadorNombre)
        {
            Console.WriteLine("Creando partida...");
            Jugador jugador = _context.Jugadores.FirstOrDefault(j => j.Nombre == jugadorNombre);
            Console.WriteLine("Buscando jugador...");

            if (jugador == null)
            {
                return null;
            }
            Console.WriteLine("Jugador encontrado: " + jugador.Nombre);

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
                JugadorId = jugador.Id,
                Partida = nuevaPartida
            };

            _context.JugadoresPartidas.Add(jugadorPartida);
            _context.SaveChanges();
            Console.WriteLine("Partida creada con exito");

            return nuevaPartida;
        }

        public Partida? ObtenerPartidaPorId(int id)
        {
            Partida partida = _context.Partidas
                .Include(p => p.PartidaJugadores)
                .ThenInclude(jp => jp.Jugador)
                .FirstOrDefault(p => p.Id == id);

            if (partida == null)
            {
                Console.WriteLine("No se encontro la partida con ID: " + id);
                return null;
            }
            Console.WriteLine("Partida encontrada con ID: " + id);

            return partida;
        }
    }
}
