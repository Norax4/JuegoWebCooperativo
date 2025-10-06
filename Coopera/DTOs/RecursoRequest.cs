using System.Text.Json.Serialization;
using Coopera.Services;

namespace Coopera.DTOs
{
    public class RecursoRequest
    {
        [JsonPropertyName("recurso")]
        public Recurso Recurso { get; set; }
    }
}

