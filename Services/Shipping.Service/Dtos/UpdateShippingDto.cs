using System.ComponentModel.DataAnnotations;

namespace Shipments.Service.Dtos
{
    public class UpdateShippingDto
    {
        public string? DeliveryPointId { get; set; } = null;

        public DateTime? DeliveredAt { get; set; } = null;

        public string? ShipmentType { get; set; }

        [Validators.VehicleId]
        public string? VehicleId { get; set; } = null;

    }
}
