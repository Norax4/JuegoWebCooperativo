using Coopera.Factories.Minijuegos;
using Coopera.Services;

namespace Coopera.Factories
{
    public class GenerarMinijuego
    {
        public static IMinijuego Generar(Recurso recurso)
        {
            switch (recurso)
            {
                case Recurso.Madera: return new MinijuegoMadera();
                case Recurso.Piedra: return new MinijuegoPiedra();
                case Recurso.Comida: return new MinijuegoComida();
                default: throw new ArgumentException("Tipo de minijuego inválido");
            }
        }
    }
}
