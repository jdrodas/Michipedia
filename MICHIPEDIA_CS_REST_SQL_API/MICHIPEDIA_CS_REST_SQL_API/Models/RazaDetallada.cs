using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_SQL_API.Models
{
    public class RazaDetallada : Raza
    {
        [JsonPropertyName("caracteristicas")]
        public List<Caracteristica>? Caracteristicas { get; set; } = null;

        [JsonPropertyName("comportamientos")]
        public List<Comportamiento>? Comportamientos{ get; set; } = null;
    }
}
