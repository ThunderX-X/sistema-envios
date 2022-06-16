using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Clients.Service.Models
{
    public class Client
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement]
        public string FirstName { get; set; }

        [BsonElement]
        public string LastName { get; set; }

        [BsonElement]
        public string Email { get; set; }

        [BsonElement]
        public string Phone { get; set; }

        [BsonElement]
        public DateTime? CreatedAt { get; set; }

        [BsonElement]
        public DateTime UpdatedAt { get; set; }
    }
}
