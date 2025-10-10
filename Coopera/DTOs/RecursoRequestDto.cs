using System.Text.Json.Serialization;
using Coopera.Services;

namespace Coopera.DTOs
{
    public class RecursoRequestDto
    {
        [JsonPropertyName("recurso")]
        public Recurso Recurso { get; set; }
    }
}

