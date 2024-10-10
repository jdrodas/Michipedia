using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Models
{
    public class Raza
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [BsonElement("nombre")]
        [JsonPropertyName("nombre")]
        [BsonRepresentation(BsonType.String)]
        public string? Nombre { get; set; } = string.Empty;

        [BsonElement("descripcion")]
        [JsonPropertyName("descripcion")]
        [BsonRepresentation(BsonType.String)]
        public string? Descripcion { get; set; } = string.Empty;

        [BsonElement("pais")]
        [JsonPropertyName("pais")]
        [BsonRepresentation(BsonType.String)]
        public string? Pais { get; set; } = string.Empty;


    }
}
