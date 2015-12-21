using ParkingATHWeb.Contracts.Common;

namespace ParkingATHWeb.Contracts.DTO.PriceTreshold
{
    public class PriceTresholdBaseDto:BaseDto
    {
        public int Id { get; set; }
        public int MinCharges { get; set; }
        public decimal PricePerCharge { get; set; }
    }
}
