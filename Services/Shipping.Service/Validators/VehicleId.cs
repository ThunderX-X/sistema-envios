using Shipments.Service.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Shipments.Service.Validators
{
    public class VehicleId :  ValidationAttribute
    {
        public string shipmentTypePropertyName;

        public VehicleId(string shipmentTypePropertyName = "ShipmentType")
        {
            this.shipmentTypePropertyName = shipmentTypePropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            PropertyInfo? vehicleTypeProperty = context.ObjectType.GetProperty(shipmentTypePropertyName);
            if (vehicleTypeProperty == null) return new ValidationResult(
                    "Típo de envio no especificado"
                );
            string? vehicleType = vehicleTypeProperty?.GetValue(context.ObjectInstance)?.ToString();
            if(vehicleType == null) return new ValidationResult(
                    "Típo de envio no especificado"
                );
            if(value == null)
                return new ValidationResult(
                    "No se ha especificado placa/numero de flota"
                );
            return ValidateVehicleId(value.ToString(), vehicleType);

        }

        private ValidationResult? ValidateVehicleId(string value, string vehicleType)
        {
            if (vehicleType.Equals(ShipmentTypes.Maritime))
                return ValidateFleetId(value);
            else if (vehicleType.Equals(ShipmentTypes.Terrestrial))
                return ValidateTerrestrialVehicleId(value);
            else
                return new ValidationResult(
                        "Invalid Vehicle type"
                    );
        }

        private ValidationResult? ValidateFleetId(string value)
        {
            ValidationResult? lengthResult = ValidateLength(value, 8);
            if (lengthResult != null)
                return lengthResult;
            ValidationResult?[] results = new ValidationResult?[]
            {
                ValidateOnlyLetters(value[0..3]),
                ValidateOnlyNumbers(value[3..7]),
                ValidateOnlyLetters(value[^1..])
            };
            foreach(var result in results) 
                if (result != null) return result;

            return null;
        }

        private ValidationResult? ValidateTerrestrialVehicleId(string value)
        {

            ValidationResult? lengthResult = ValidateLength(value, 6);
            if (lengthResult != null) 
                return lengthResult;
            ValidationResult?[] results = {
                ValidateOnlyLetters(value[..3]),
                ValidateOnlyNumbers(value[^3..])
            };
            foreach (var result in results)
                if (result != null) return result;

            return null;
        }

        private ValidationResult? ValidateLength(string value, int length)
        {
            if (value.Length != length) return new ValidationResult(
                    $"Length ({value.Length}) does not meet expectations ({length})"
                );

            return null;
        }

        private ValidationResult?  ValidateOnlyLetters(string value)
        {
            if (!value.All(Char.IsLetter)) return new ValidationResult(
                    $"The value {value} must contain only letters"
                );

            return null;
        }

        private ValidationResult? ValidateOnlyNumbers(string value)
        {
            if (!value.All(Char.IsNumber)) return new ValidationResult(
                    $"The value {value} must contain only numbers"
                );

            return null;
        }
    }
}
