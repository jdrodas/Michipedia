using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Models
{
    public class RazaDetallada : Raza
    {
        [JsonPropertyName("caracteristicas")]
        public List<CaracteristicaSimplificada>? Caracteristicas { get; set; } = null;

        [JsonPropertyName("comportamientos")]
        public List<Comportamiento>? Comportamientos { get; set; } = null;
    }
}
