namespace Coopera.Models
{
	public class Partida
	{
		public int ID { get; set; }
		public int MetaComida { get; set; }
		public int MetaMadera { get; set; }
		public int MetaPiedra { get; set; }
		public List<RegistroRecursos>? RegistroIngresos { get; set; }
		public List<JugadorPartida>? Jugadores { get; set; }
	}
}
