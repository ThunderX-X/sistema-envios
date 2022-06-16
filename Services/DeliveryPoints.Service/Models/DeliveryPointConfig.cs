using DeliveryPoints.Service.Interfaces;

namespace DeliveryPoints.Service.Models
{
    public class DeliveryPointConfig : MongoConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}
