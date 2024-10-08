using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Models
{
    public class Caracteristica
    {
        [JsonPropertyName("uuid")]
        public Guid Uuid { get; set; } = Guid.Empty;

        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; } = string.Empty;

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; } = string.Empty;
    }
}
