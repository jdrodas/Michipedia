using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_SQL_API.Models
{
    public class Comportamiento
    {
        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; } = string.Empty;

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; } = string.Empty;

        [JsonPropertyName("nivel")]
        public string? Nivel { get; set; } = string.Empty;

        [JsonPropertyName("valoracion")]
        public string? Valoracion { get; set; } = string.Empty;
    }
}
