using MongoDB.Bson.Serialization.Attributes;

namespace DeliveryPoints.Service.Models
{
    public class DeliveryPoint
    {
        [BsonId]
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement]
        public string Name { get; set; }
        
        [BsonElement]
        public string DeliveryPointType { get; set; }

        [BsonElement]
        public DateTime CreatedAt { get; set; }
        
        [BsonElement]
        public DateTime UpdatedAt { get; set; }

    }
}
