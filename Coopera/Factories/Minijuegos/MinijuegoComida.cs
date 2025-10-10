namespace Coopera.Factories.Minijuegos
{
    public class MinijuegoComida : IMinijuego
    {
        private readonly string[] Proposiciones = new string[]
        {
                    "Exactamente 1 número es par",
                    "Exactamente 2 números son pares",
                    "Los 3 números son pares",
                    "La suma de los 3 números es par",
                    "La suma de los 3 números es impar",
                    "La suma de los 3 números es mayor a 100",
                    "La suma de los 3 números es menor a 100",
                    "Al menos 1 número es mayor a 50",
                    "Al menos 1 número es menor a 50",
                    "Todos los números son diferentes"
                };

        public int[] SecuenciaNumerica { get; set; } = new int[3];
        public string Pregunta { get; set; }
        public int RespuestaCorrecta { get; set; }

        public MinijuegoComida()
        {
            Random rand = new Random();

            for (int i = 0; i < SecuenciaNumerica.Length; i++)
            {
                SecuenciaNumerica[i] = rand.Next(1, 100);
            }

            int indice = rand.Next(0, Proposiciones.Length);
            Pregunta = Proposiciones[indice];

            int counter = 0;
            HashSet<int> elementosUnicos = new HashSet<int>();

            if (Pregunta == Proposiciones[0])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += (num % 2) == 0 ? 1 : 0;
                }
                RespuestaCorrecta = counter == 1 ? 1 : 0;
            }
            else if (Pregunta == Proposiciones[1])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += (num % 2) == 0 ? 1 : 0;
                }
                RespuestaCorrecta = counter == 2 ? 1 : 0;
            }
            else if (Pregunta == Proposiciones[2])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += (num % 2) == 0 ? 1 : 0;
                }
                RespuestaCorrecta = counter == 3 ? 1 : 0;
            }
            else if (Pregunta == Proposiciones[3])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += num;
                }
                RespuestaCorrecta = (counter % 2) == 0 ? 1 : 0;
            }
            else if (Pregunta == Proposiciones[4])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += num;
                }
                RespuestaCorrecta = (counter % 2) != 0 ? 1 : 0;
            }
            else if (Pregunta == Proposiciones[5])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += num;
                }
                RespuestaCorrecta = counter > 100 ? 1 : 0;
            }
            else if (Pregunta == Proposiciones[6])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += num;
                }
                RespuestaCorrecta = counter < 100 ? 1 : 0;
            }
            else if (Pregunta == Proposiciones[7])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    if (num > 50)
                        counter++;
                }
                RespuestaCorrecta = counter >= 1 ? 1 : 0;
            }
            else if (Pregunta == Proposiciones[8])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    if (num < 50)
                        counter++;
                }
                RespuestaCorrecta = counter >= 1 ? 1 : 0;
            }
            else if (Pregunta == Proposiciones[9])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    if (elementosUnicos.Add(num))
                        counter++;
                }
                RespuestaCorrecta = counter == SecuenciaNumerica.Length ? 1 : 0;
            }
        }
    }
}
