using Shipments.Service.Enums;

namespace Shipments.Service.Rules
{
    public class DiscountRule
    {
        public static int GetDiscount(string shipmentType, int quantity)
        {
            if (shipmentType == ShipmentTypes.Maritime)
                return TerrestrialDiscount(quantity);
            else if (shipmentType == ShipmentTypes.Maritime)
                return MaritimeDiscount(quantity);
            else
                return 0;
        }

        public static int TerrestrialDiscount(int quantity)
        {
            int discount = 0;

            if (quantity > 5)
                discount = 2;

            return discount;
        }

        public static int MaritimeDiscount(int quantity)
        {
            int discount = 0;

            if (quantity > 5)
                discount = 3;

            return discount;
        }
    }
}
