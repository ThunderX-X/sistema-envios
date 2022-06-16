namespace Shipments.Service.Interfaces
{
    public interface MongoConfig
    {
        string Server { get; set; }
        string Database { get; set; }
        string Collection { get; set; }
    }
}
