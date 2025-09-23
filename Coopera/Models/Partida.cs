using System.Collections;
using Coopera.Enums;

namespace Coopera.Models
{
    public class Partida
    {
        public int Id { get; set; }
        public Dificultad Dificultad { get; set; }
        public int Meta { get; set; }
        public List<Recurso> Madera { get; set; } = new List<Recurso>();
        public List<Recurso> Piedra { get; set; } = new List<Recurso>();
        public List<Recurso> Comida { get; set; } = new List<Recurso>();
        private DateTime _tiempoInicio;
        private DateTime? _tiempoFinal;
        public TimeSpan Duracion
        {
            get
            {
                if (_tiempoFinal.HasValue)
                {
                    return _tiempoFinal.Value - _tiempoInicio;
                }
                return DateTime.Now - _tiempoInicio;
            }
        }
        public ICollection<Jugador> Jugadores { get; set; } = new List<Jugador>();
        public ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();

        public Partida(){}
        public Partida(Dificultad dificultad)
        {
            Dificultad = dificultad;
            Meta = CalcularMeta(null);
            Iniciar();
        }

        public void Iniciar()
        {
            _tiempoInicio = DateTime.Now;
            _tiempoFinal = null;
        }

        public void Finalizar()
        {
            _tiempoFinal = DateTime.Now;
        }

        public int CalcularMeta(int? seedInput)
        {
            int seed = seedInput ?? (int)DateTime.Now.Ticks;

            Random rng = new Random(seed);

            switch ((int)Dificultad)
            {
                case 0:
                    return rng.Next(10, 31);
                case 1:
                    return rng.Next(30, 51);
                case 2:
                    return rng.Next(50, 101);
                default:
                    throw new InvalidOperationException(
                        $"Dificultad no válida."
                    );
            }
        }

        public bool ChequearMeta(List<Recurso> recursos)
        {
            if (recursos.Count >= Meta)
            {
                return true;
            }
            return false;
        }
    }
}