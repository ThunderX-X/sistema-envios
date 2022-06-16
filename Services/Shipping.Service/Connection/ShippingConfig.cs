using Shipments.Service.Interfaces;

namespace Shipments.Service.Connection
{
    public class ShippingConfig : MongoConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}
