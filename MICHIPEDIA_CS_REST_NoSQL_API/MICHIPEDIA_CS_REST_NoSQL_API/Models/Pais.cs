using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Models
{
    public class Pais
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [BsonElement("nombre")]
        [JsonPropertyName("nombre")]
        [BsonRepresentation(BsonType.String)]
        public string? Nombre { get; set; } = string.Empty;


        [BsonElement("continente")]
        [JsonPropertyName("continente")]
        [BsonRepresentation(BsonType.String)]
        public string? Continente { get; set; } = string.Empty;
    }
}
