using System.Text.Json.Serialization;

namespace Coopera.DTOs
{
    public class MinijuegoResponseDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("tipo")]
        public string Tipo { get; set; } = string.Empty;
        [JsonPropertyName("numeros")]
        public List<string> Numeros { get; set; } = [];
        [JsonPropertyName("pregunta")]
        public string? Pregunta_Texto { get; set; } = string.Empty;
        [JsonPropertyName("codigo_pregunta")]
        public string Codigo_Pregunta { get; set; } = string.Empty;
        [JsonPropertyName("fecha_creacion")]
        public string FechaCreacion { get; set; } = string.Empty;
    }
}