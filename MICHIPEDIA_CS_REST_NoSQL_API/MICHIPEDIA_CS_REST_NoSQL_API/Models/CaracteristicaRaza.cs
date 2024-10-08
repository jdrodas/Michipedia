using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_SQL_API.Models
{
    public class CaracteristicaRaza
    {
        [JsonPropertyName("raza_uuid")]
        public Guid Raza_Uuid { get; set; } = Guid.Empty;

        [JsonPropertyName("raza_nombre")]
        public string? Raza_Nombre { get; set; } = string.Empty;

        [JsonPropertyName("valoracion")]
        public string? Valoracion { get; set; } = string.Empty;
    }
}
