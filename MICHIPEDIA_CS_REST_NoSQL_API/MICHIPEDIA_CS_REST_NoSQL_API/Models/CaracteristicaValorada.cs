using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Models
{
    public class CaracteristicaValorada : Caracteristica
    {
        [JsonPropertyName("valoracion_caracteristicas")]
        public List<CaracteristicaRaza>? Valoracion_Caracteristicas { get; set; } = null;
    }
}
