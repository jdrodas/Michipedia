using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Models
{
    public class CaracteristicaRaza
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [BsonElement("raza_id")]
        [JsonPropertyName("raza_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RazaId { get; set; } = string.Empty;

        [BsonElement("caracteristica_id")]
        [JsonPropertyName("caracteristica_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CaracteristicaId { get; set; } = string.Empty;

        [BsonElement("valoracion")]
        [JsonPropertyName("valoracion")]
        [BsonRepresentation(BsonType.String)]
        public string? Valoracion { get; set; } = string.Empty;
    }
}
