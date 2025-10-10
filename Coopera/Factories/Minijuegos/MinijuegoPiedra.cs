namespace Coopera.Factories.Minijuegos
{
    public class MinijuegoPiedra : IMinijuego
    {
        private readonly string[] Preguntas = new string[]
        {
                    "¿Había exactamente 2 números pares?",
                    "¿Había exactamente 3 números pares?",
                    "¿La suma de todos los números superaba 50?",
                    "¿La suma de todos los números superaba 30?",
                    "¿La suma de todos los números superaba 70?",
                    "¿Había 2 números iguales?",
                    "¿Había 3 números iguales?",
                    "¿Había 4 números iguales?",
                    "¿Había algún número menor a 10?",
                    "¿Había algún número menor a 15?",
                    "¿Había algún número menor a 5?",
                };

        public int[] SecuenciaNumerica { get; set; } = new int[5];
        public string Pregunta { get; set; }
        public int RespuestaCorrecta { get; set; }

        public MinijuegoPiedra()
        {
            Random rand = new Random();

            for (int i = 0; i < SecuenciaNumerica.Length; i++) 
            { 
                SecuenciaNumerica[i] = rand.Next(1,100);
            }

            int indice = rand.Next(0, Preguntas.Length);
            Pregunta = Preguntas[indice];

            int counter = 0;
            HashSet<int> elementosUnicos = new HashSet<int>();

            if (Pregunta == Preguntas[0])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += (num % 2) == 0 ? 1 : 0;
                }

                RespuestaCorrecta = counter == 2 ? 1 : 0;
            } else if (Pregunta == Preguntas[1])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += (num % 2) == 0 ? 1 : 0;
                }

                RespuestaCorrecta = counter == 3 ? 1 : 0;
            } else if (Pregunta == Preguntas[2])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += num;
                }

                RespuestaCorrecta = counter > 50 ? 1 : 0;
            } else if (Pregunta == Preguntas[3])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += num;
                }

                RespuestaCorrecta = counter > 30 ? 1 : 0;
            } else if (Pregunta == Preguntas[4])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    counter += num;
                }

                RespuestaCorrecta = counter > 70 ? 1 : 0;
            } else if (Pregunta == Preguntas[5])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    if (!elementosUnicos.Add(num)) // Add devuelve false si el elemento ya existe
                    {
                        counter++;
                    }
                }

                RespuestaCorrecta = counter == 2 ? 1 : 0;
            } else if (Pregunta == Preguntas[6])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    if (!elementosUnicos.Add(num)) 
                    {
                        counter++;
                    }
                }

                RespuestaCorrecta = counter == 3 ? 1 : 0;
            } else if (Pregunta == Preguntas[7])
            {
                foreach (int num in SecuenciaNumerica)
                {
                    if (!elementosUnicos.Add(num)) 
                    {
                        counter++;
                    }
                }

                RespuestaCorrecta = counter == 4 ? 1 : 0;
            } else if (Pregunta == Preguntas[8])
            {
                RespuestaCorrecta = SecuenciaNumerica.Any(n => n < 10) ? 1 : 0;
            } else if (Pregunta == Preguntas[9])
            {
                RespuestaCorrecta = SecuenciaNumerica.Any(n => n < 15) ? 1 : 0;
            } else if (Pregunta == Preguntas[10])
            {
                RespuestaCorrecta = SecuenciaNumerica.Any(n => n < 5) ? 1 : 0;
            }
        }
    }
}
