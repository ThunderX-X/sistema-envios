using MongoDB.Driver;
using Shipments.Service.Interfaces;
using Shipments.Service.Models;

namespace Shipments.Service.Connection
{
    public class ShippingConnection
    {
        MongoConfig config;
        MongoClient client;
        IMongoDatabase database;
        public IMongoCollection<Shipping> Collection
        {
            get => database.GetCollection<Shipping>(config.Collection);
        }

        public ShippingConnection(MongoConfig config)
        {
            this.config = config;
            InitializeClient();
            InitializeDatabase();
        }

        private void InitializeClient()
        {
            this.client = new MongoClient(config.Server);
        }

        private void InitializeDatabase()
        {
            database = client.GetDatabase(config.Database);
        }
    }
}
