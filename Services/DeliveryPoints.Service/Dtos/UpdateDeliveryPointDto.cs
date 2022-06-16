using System.ComponentModel.DataAnnotations;

namespace DeliveryPoints.Service.Dtos
{
    public class UpdateDeliveryPointDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
    }
}
