using System.Collections;

namespace Coopera.Models
{
    public enum Dificultad
    {
        Facil = 0,
        Media = 1,
        Dificil = 2
    }
    public class Partida
    {
        public int Id { get; set; }
        public Dificultad Dificultad { get; set; }
        public int Meta { get; set; }
        public int Madera { get; set; }
        public int Piedra { get; set; }
        public int Comida { get; set; }
        private DateTime _tiempoInicio;
        private DateTime? _tiempoFinal;
        public bool PartidaFinalizada
        {
            get
            {
                if (ChequearPartidaFinalizada())
                {
                    Finalizar();
                    return true;
                }
                return false;
            }
        }
        public Double Duracion
        {
            get
            {
                if (_tiempoFinal.HasValue)
                {
                    return (_tiempoFinal.Value - _tiempoInicio).TotalSeconds;
                }
                return (DateTime.Now - _tiempoInicio).TotalSeconds;
            }
        }
        public ICollection<JugadorPartida> PartidaJugadores { get; set; } = new List<JugadorPartida>();

        protected Partida() { }
        public Partida(Dificultad dificultad)
        {
            if (!Enum.IsDefined(typeof(Dificultad), dificultad))
            {
                throw new ArgumentException("Debe ingresar una dificultad valida.", nameof(dificultad));
            }

            Dificultad = dificultad;
            Meta = CalcularMeta(null);
            Iniciar();
        }

        private void Iniciar()
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

        public bool ChequearMetaRecurso(int recurso)
        {
            if (recurso >= Meta)
            {
                return true;
            }
            return false;
        }

        private bool ChequearPartidaFinalizada()
        {
            bool metaMaderaAlcanzada = ChequearMetaRecurso(Madera);
            bool metaPiedraAlcanzada = ChequearMetaRecurso(Piedra);
            bool metaComidaAlcanzada = ChequearMetaRecurso(Comida);

            if (metaMaderaAlcanzada && metaPiedraAlcanzada && metaComidaAlcanzada)
            {
                return true;
            }

            return false;
        }
    }
}
