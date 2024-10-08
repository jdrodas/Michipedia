using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_SQL_API.Models
{
    public class Pais
    {
        [JsonPropertyName("uuid")]
        public Guid Uuid { get; set; } = Guid.Empty;

        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; } = string.Empty;

        [JsonPropertyName("continente")]
        public string? Continente { get; set; } = string.Empty;
    }
}
