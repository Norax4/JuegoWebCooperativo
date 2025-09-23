namespace Coopera.Models
{
    public class JugadorRecurso
    {
        public int JugadorId { get; set; }
        public Jugador? Jugador { get; set; }

        public int RecursoId { get; set; }
        public Recurso? Recurso { get; set; }
    }
}