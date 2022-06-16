    using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Shipments.Service.Dtos
{
    public class CreateShippingDto
    {
        [Required]
        public string Client { get; set; }

        [Required]
        public string ProductType { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Validators.ShipmentType("El tipo de envio es invalido, solo se acepta 'Maritimo' y 'Terrestre'")]
        public string ShipmentType { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double Price { get; set; }
    }
}
