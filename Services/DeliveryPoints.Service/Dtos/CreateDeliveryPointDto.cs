using System.ComponentModel.DataAnnotations;

namespace DeliveryPoints.Service.Dtos
{
    public class CreateDeliveryPointDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public string DeliveryPointType { get; set; }
    }
}
