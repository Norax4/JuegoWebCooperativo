namespace Coopera.Models
{
	public class Recurso
	{
		public int ID { get; set; }
		public string Material { get; set; }
		public List<RegistroRecursos> Registros { get; set; }
	}
}
