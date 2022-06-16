using Shipments.Service.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shipments.Service.Validators
{
    public class ShipmentType : ValidationAttribute
    {


        private static readonly List<string> validValues = new()
        {
            ShipmentTypes.Terrestrial,
            ShipmentTypes.Maritime
        };

        public ShipmentType() : base()
        {
        }

        public ShipmentType(string errorMessage) : base(errorMessage)
        {
        }

        public override bool IsValid(object? value)
        {
            string? valueAsString = value?.ToString();
            if (valueAsString == null) return false;
            bool isValid = validValues.Contains(valueAsString);

            return isValid;
        }
    }
}
