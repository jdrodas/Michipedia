using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Models
{
    public class Resumen
    {
        [JsonPropertyName("razas")]
        public long Razas { get; set; } = 0;

        [JsonPropertyName("paises")]
        public long Paises { get; set; } = 0;

        [JsonPropertyName("caracteristicas")]
        public long Caracteristicas { get; set; } = 0;

        [JsonPropertyName("comportamientos")]
        public long Comportamientos { get; set; } = 0;
    }
}