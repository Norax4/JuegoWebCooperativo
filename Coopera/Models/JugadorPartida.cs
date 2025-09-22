namespace Coopera.Models
{
	public class JugadorPartida
	{
		public int ID { get; set; }
		public int JugadorID { get; set; }
		public int PartidaID { get; set; }
		// public DateTime HoraConexion { get; set; } ??
		public Jugador? Jugador { get; set; }
		public Partida? Partida { get; set; }
	}
}
