using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_SQL_API.Models
{
    public class RazaCaracterizada : Raza
    {
        [JsonPropertyName("caracteristicas")]
        public List<Caracteristica>? Caracteristicas { get; set; } = null;
    }
}
