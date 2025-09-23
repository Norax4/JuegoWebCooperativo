using System.Collections;

namespace Coopera.Models
{
    public class Partida
    {
        public int Id { get; set; }
        public string Dificultad { get; set; } = "facil";
        public int Meta { get; set; }
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

            switch (Dificultad.ToLower())
            {
                case "facil":
                    return rng.Next(10, 31);
                case "media":
                    return rng.Next(30, 51);
                case "dificil":
                    return rng.Next(50, 101);
                default:
                    throw new InvalidOperationException(
                        $"Dificultad '{Dificultad}' no es válida."
                    );
            }
        }
    }
}