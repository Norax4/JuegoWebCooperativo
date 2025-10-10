using Coopera.Data;
using Coopera.Factories;
using Coopera.Factories.Minijuegos;
using Coopera.Models;
using Microsoft.EntityFrameworkCore;

namespace Coopera.Services
{
    public enum Recurso
    {
        Madera = 0,
        Piedra = 1,
        Comida = 2,
    }
    public class PartidaService
    {
        private readonly AppDbContext _context;

        public PartidaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task NuevaPartida(Dificultad dificultad, string nombreJugador, ISession session)
        {
            //Chequear si existe partida en curso
            if (session.GetInt32("PartidaId") != null)
            {
                throw new ArgumentException("Ya hay una partida en curso.");
            }

            Jugador jugador = new(nombreJugador);
            Partida partida = new Partida(dificultad);

            _context.Jugadores.Add(jugador);
            _context.Partidas.Add(partida);

            await _context.SaveChangesAsync();

            JugadorPartida jugadorPartida = new(jugador.Id, partida.Id);

            _context.JugadoresPartidas.Add(jugadorPartida);

            await _context.SaveChangesAsync();

            // Guardar Id de partida y jugador en sesion
            session.SetInt32("PartidaId", partida.Id);
            session.SetInt32("JugadorId", jugador.Id);
        }

        public async Task<JugadorPartida> AumentarRecurso(int? partidaId, int? jugadorId, Recurso recurso)
        {
            JugadorPartida? jugadorPartida = await _context.JugadoresPartidas
            .Include(jp => jp.Partida)
            .FirstOrDefaultAsync(jp => jp.PartidaId == partidaId && jp.JugadorId == jugadorId);

            if (jugadorPartida == null)
            {
                throw new ArgumentException("Jugador o partida no encontrados");
            }

            switch (recurso)
            {
                case Recurso.Madera:
                    if (jugadorPartida.Partida!.ChequearMetaRecurso(jugadorPartida.Partida.Madera) != true)
                    {
                        jugadorPartida.MaderaJugador++;
                        jugadorPartida.Partida.Madera++;
                        break;
                    }
                    break;
                case Recurso.Piedra:
                    if (jugadorPartida.Partida!.ChequearMetaRecurso(jugadorPartida.Partida.Piedra) != true)
                    {
                        jugadorPartida.PiedraJugador++;
                        jugadorPartida.Partida.Piedra++;
                        break;
                    }
                    break;
                case Recurso.Comida:
                    if (jugadorPartida.Partida!.ChequearMetaRecurso(jugadorPartida.Partida.Comida) != true)
                    {
                        jugadorPartida.ComidaJugador++;
                        jugadorPartida.Partida.Comida++;
                        break;
                    }
                    break;
            }

            await _context.SaveChangesAsync();

            return jugadorPartida;
        }

        public async Task<IMinijuego> CrearMiniJuego(int? partidaId, int? jugadorId, Recurso recurso)
        {
            JugadorPartida? jugadorPartida = await _context.JugadoresPartidas
                .Include(jp => jp.Partida)
                .FirstOrDefaultAsync(jp => jp.PartidaId == partidaId && jp.JugadorId == jugadorId);

            if (jugadorPartida == null)
                throw new ArgumentException("Jugador o partida no encontrados");

            IMinijuego minijuego = GenerarMinijuego.Generar(recurso);

            return minijuego;
        }
    }
}
