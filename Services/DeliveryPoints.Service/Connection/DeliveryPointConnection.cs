using DeliveryPoints.Service.Interfaces;
using DeliveryPoints.Service.Models;
using MongoDB.Driver;

namespace DeliveryPoints.Service.Connection
{
    public class DeliveryPointConnection
    {
        MongoConfig config;
        MongoClient client;
        IMongoDatabase database;
        public IMongoCollection<DeliveryPoint> Collection
        {
            get => database.GetCollection<DeliveryPoint>(config.Collection);
        }

        public DeliveryPointConnection(MongoConfig config)
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
