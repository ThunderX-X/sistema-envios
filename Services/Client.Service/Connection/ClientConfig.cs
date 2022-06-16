using Clients.Service.Interfaces;

namespace Clients.Service.Connection
{
    public class ClientConfig : MongoConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}
