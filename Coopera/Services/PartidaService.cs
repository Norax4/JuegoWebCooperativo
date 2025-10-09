using Coopera.Data;
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
        private readonly string[] preguntas = new string[]
                {
                    "¿Había exactamente 2 números pares?",
                    "¿Había exactamente 3 números pares?",
                    "¿Había exactamente 4 números pares?",
                    "¿La suma de todos los números superaba 50?",
                    "¿La suma de todos los números superaba 30?",
                    "¿La suma de todos los números superaba 10?",
                    "¿La suma de todos los números superaba 70?",
                    "¿La suma de todos los números superaba 90?",
                    "¿Había 2 números iguales?",
                    "¿Había 3 números iguales?",
                    "¿Había 4 números iguales?",
                    "¿Había algún número menor a 10?",
                    "¿Había algún número menor a 15?",
                    "¿Había algún número menor a 7?",
                    "¿Había algún número menor a 5?",
                    "¿Había algún número menor a 3?"
                };

        private readonly string[] proposiciones = new string[]
                {
                    "Exactamente 1 número es par",
                    "Exactamente 2 números son pares",
                    "Exactamente 3 números son pares",
                    "La suma de los 3 números es par",
                    "La suma de los 3 números es impar",
                    "La suma de los 3 números es mayor a 100",
                    "La suma de los 3 números es menor a 100",
                    "Al menos 1 número es mayor a 50",
                    "Al menos 1 número es menor a 50",
                    "Todos los números son diferentes"
                };

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

        public int[] crearArrayMinijuegos(Recurso recurso)
        {
            Random rand = new Random();
            int largoArray = recurso == Recurso.Piedra ? 5 : 3;
            int[] arrayMinijuego = new int[largoArray];
            for (int i = 0; i < largoArray; i++)
            {
                arrayMinijuego[i] = rand.Next(1, 100);
            }
            return arrayMinijuego;
        }

        public string GenerarPreguntaAleatoria(Recurso recurso)
        {
            if (recurso == Recurso.Piedra)
            {
                Random rand = new Random();
                int indice = rand.Next(preguntas.Length);

                return preguntas[indice];
            } else
            {
                Random rand = new Random();
                int indice = rand.Next(proposiciones.Length);

                return proposiciones[indice];
            }
        }
    }
}
