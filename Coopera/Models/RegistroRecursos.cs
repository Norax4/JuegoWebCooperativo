namespace Coopera.Models
{
	public class RegistroRecursos
	{
		public int ID { get; set; }
		public int PartidaID { get; set; }
		public int RecursoID { get; set; }
		public int Cantidad { get; set; }
		public DateTime FechaRegistro { get; set; }
		public Partida? partida { get; set; }
		public Recurso? recurso { get; set; }
	}
}
