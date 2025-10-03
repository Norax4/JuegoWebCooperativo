using System.Text.Json.Serialization;
using Coopera.Models;

namespace Coopera.DTOs
{
    public class RecursoRequest
    {
        [JsonPropertyName("recurso")]
        public Recurso Recurso { get; set; }
    }
}

