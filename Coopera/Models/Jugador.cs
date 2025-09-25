namespace Coopera.Models
{
    public class Jugador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public ICollection<JugadorPartida> JugadorPartidas { get; set; } = new List<JugadorPartida>();

        public Jugador() { }
        public Jugador(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("No puede dejar el nombre vacío", nameof(nombre));
            }

            Nombre = nombre;
        }
    }
}