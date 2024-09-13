using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_SQL_API.Models
{
    public class Resumen
    {
        [JsonPropertyName("razas")]
        public int Razas { get; set; } = 0;

        [JsonPropertyName("paises")]
        public int Paises { get; set; } = 0;

        [JsonPropertyName("caracteristicas")]
        public int Caracteristicas { get; set; } = 0;

        [JsonPropertyName("comportamientos")]
        public int Comportamientos { get; set; } = 0;
    }
}