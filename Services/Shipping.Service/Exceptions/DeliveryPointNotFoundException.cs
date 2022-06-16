namespace Shipments.Service.Exceptions
{
    public class DeliveryPointNotFoundException : Exception
    {
        public DeliveryPointNotFoundException(string? message) : base(message)
        {
        }
    }
}
