using ParkingATHWeb.Contracts.Common;

namespace ParkingATHWeb.Contracts.DTO.PriceTreshold
{
    public class PriceTresholdBaseDto:BaseDto<int>
    {
        public int MinCharges { get; set; }
        public decimal PricePerCharge { get; set; }
        public bool IsDeleted { get; set; }
    }
}
