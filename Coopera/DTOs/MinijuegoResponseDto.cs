using System.Text.Json.Serialization;

namespace Coopera.DTOs
{
    public class MinijuegoResponseDto
    {
        [JsonPropertyName("tipo")]
        public required string Tipo { get; set; }
        [JsonPropertyName("numeros")]
        public required List<string> Numeros { get; set; }
        [JsonPropertyName("pregunta")]
        public required string? Pregunta_Texto { get; set; }
        [JsonPropertyName("codigo_pregunta")]
        public required string Codigo_Pregunta { get; set; } //Guarda "MATEMATICA" si es pregunta de Matematicas, o el codigo que corresponde
                                                             //a la pregunta de Logica o Memoria, ej; "2PARES", "SECUENCIA_MAYOR", "SECUENCIA_MENOR", etc.
        [JsonPropertyName("fecha_creacion")]
        public required string FechaCreacion { get; set; }
    }
}