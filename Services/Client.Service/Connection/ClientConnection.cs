using Clients.Service.Interfaces;
using Clients.Service.Models;
using MongoDB.Driver;

namespace Clients.Service.Connection
{
    public class ClientConnection
    {
        MongoConfig config;
        MongoClient client;
        IMongoDatabase database;
        public IMongoCollection<Client> Collection {
            get => database.GetCollection<Client>(config.Collection);
        }

    public ClientConnection(MongoConfig config)
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
