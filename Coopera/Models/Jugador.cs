namespace Coopera.Models
{
    public class Jugador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int PartidaId { get; set; }
        public Partida Partida { get; set; } = new();
        public ICollection<JugadorRecurso> JugadorRecursos { get; set; } = new List<JugadorRecurso>();
    }
}