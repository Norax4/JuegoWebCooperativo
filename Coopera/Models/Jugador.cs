namespace Coopera.Models
{
	public class Jugador
	{
		public int ID { get; set; }
		public string Nombre { get; set; }
		public List<JugadorPartida> PartidasJugadas { get; set; }
	}
}
