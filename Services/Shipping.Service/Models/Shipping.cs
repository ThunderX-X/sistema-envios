using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Shipments.Service.Models
{
    [BsonIgnoreExtraElements]
    public class Shipping
    {
        [BsonId]
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Client { get; set; }

        [BsonElement]
        public string ProductType { get; set; }

        [BsonElement]
        public int Quantity { get; set; }

        [BsonElement]
        public string ShipmentType { get; set; }

        [BsonElement]
        public DateTime RegistredAt { get; set; }

        [BsonElement]
        [BsonDefaultValue(null)]
        public DateTime? DeliveredAt { get; set; } = null;

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonDefaultValue(null)]
        public string? DeliveryPointId { get; set; }

        [BsonElement]
        public double Price { get; set; }

        [BsonElement]
        [Range(0, 100)]
        public double DiscountPercent { get; set; } = 0;

        [BsonElement]
        public double PriceWithDiscount { get; set; }

        [BsonElement]
        [BsonDefaultValue(null)]
        public string? VehicleId { get; set; } = null;

        [BsonElement]
        public string GuideNumber { get; set; }

        [BsonElement]
        public DateTime CreatedAt { get; set; }

        [BsonElement]
        public DateTime UpdatedAt { get; set; }

    }
}

