namespace Coopera.Factories.Minijuegos
{
    public class MinijuegoMadera : IMinijuego
    {
        public string Pregunta { get; set; }
        public int RespuestaCorrecta { get; set; }

        public MinijuegoMadera()
        {
            Random rand = new Random();
            int a = rand.Next(0, 100);
            int b = rand.Next(0, 100);
            int c = rand.Next(0, 100);
            int correct = a + b + c;

            Pregunta = $"{a} + {b} + {c} = ?";

            RespuestaCorrecta = correct;
        }
    }
}
