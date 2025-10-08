namespace Coopera.Models
{
    public class JugadorPartida
    {
        public int Id { get; set; }
        public int MaderaJugador { get; set; }
        public int PiedraJugador { get; set; }
        public int ComidaJugador { get; set; }
        public int JugadorId { get; set; }
        public Jugador? Jugador { get; set; }
        public int PartidaId { get; set; }
        public Partida? Partida { get; set; }

        public JugadorPartida() { }

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
    }
}
