using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_SQL_API.Models
{
    public class CaracteristicaSimplificada
    {
        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; } = string.Empty;

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; } = string.Empty;

        [JsonPropertyName("valoracion")]
        public string? Valoracion { get; set; } = string.Empty;

    }
}
