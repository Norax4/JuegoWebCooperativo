using System.Text.Json.Serialization;

namespace JuegoWebCooperativo.DTOs
{
    public class RespuestaResponseDto
    {
        [JsonPropertyName("esCorrecto")]
        public string EsCorrecto { get; set; } = string.Empty;
        [JsonPropertyName("mensaje")]
        public string Mensaje { get; set; } = string.Empty;
        [JsonPropertyName("respuestaCorrecta")]
        public string RespuestaCorrecta { get; set; } = string.Empty;
        [JsonPropertyName("tipoMinijuego")]
        public string TipoMinijuego { get; set; } = string.Empty;
    }
}