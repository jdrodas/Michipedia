using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Models
{
    public class ComportamientoRaza
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [BsonElement("raza_id")]
        [JsonPropertyName("raza_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RazaId { get; set; } = string.Empty;

        [BsonElement("comportamiento_id")]
        [JsonPropertyName("comportamiento_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ComportamientoId { get; set; } = string.Empty;

        [BsonElement("nivel")]
        [JsonPropertyName("nivel")]
        [BsonRepresentation(BsonType.String)]
        public string? Nivel { get; set; } = string.Empty;

        [BsonElement("valoracion")]
        [JsonPropertyName("valoracion")]
        [BsonRepresentation(BsonType.String)]
        public string? Valoracion { get; set; } = string.Empty;
    }
}
