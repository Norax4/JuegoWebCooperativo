namespace Coopera.Models
{
    public enum Recurso
    {
        Madera = 0,
        Piedra = 1,
        Comida = 2,
    }
    public class JugadorPartida
    {
        public int MaderaJugador { get; set; }
        public int PiedraJugador { get; set; }
        public int ComidaJugador { get; set; }
        public int JugadorId { get; set; }
        public Jugador? Jugador { get; set; }
        public int PartidaId { get; set; }
        public Partida? Partida { get; set; }

        public JugadorPartida(int jugadorId, int partidaId)
        {
            if (jugadorId <= 0) throw new ArgumentException("JugadorId inválido.");
            if (partidaId <= 0) throw new ArgumentException("PartidaId inválido.");

            JugadorId = jugadorId;
            PartidaId = partidaId;
            MaderaJugador = 0;
            PiedraJugador = 0;
            ComidaJugador = 0;
        }

        public void SumarRecurso(Recurso recurso)
        {
            switch (recurso)
            {
                case Recurso.Madera:
                    if (Partida!.ChequearMetaRecurso(Partida.Madera) != true)
                    {
                        MaderaJugador += 1;
                        Partida!.Madera += 1;
                        break;
                    }
                    break;
                case Recurso.Piedra:
                    if (Partida!.ChequearMetaRecurso(Partida.Piedra) != true)
                    {
                        PiedraJugador += 1;
                        Partida!.Piedra += 1;
                        break;
                    }
                    break;
                case Recurso.Comida:
                    if (Partida!.ChequearMetaRecurso(Partida.Comida) != true)
                    {
                        ComidaJugador += 1;
                        Partida!.Comida += 1;
                        break;
                    }
                    break;
            }
        }
    }
}