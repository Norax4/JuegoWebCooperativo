using Coopera.Enums;

namespace Coopera.Models
{
    public class Recurso
    {
        public int Id { get; set; }
        public TipoRecurso Tipo { get; set; }
        public int PartidaId { get; set; }
        public Partida Partida { get; set; } = new();
        public ICollection<JugadorRecurso> JugadorRecursos { get; set; } = new List<JugadorRecurso>();
    }
}