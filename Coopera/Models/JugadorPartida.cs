namespace Coopera.Models
{
    public class JugadorPartida
    {
        public int MaderaJugador { get; set; }
        public int PiedraJugador { get; set; }
        public int ComidaJugador { get; set; }
        public int JugadorId { get; set; }
        public Jugador? Jugador { get; set; }
        public int PartidaId { get; set; }
        public Partida? Partida { get; set; }
    }
}
