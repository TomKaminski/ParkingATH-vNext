using ParkingATHWeb.Contracts.DTO.PriceTreshold;

namespace ParkingATHWeb.Business.Tests.Base
{
    public abstract class BusinessTestBase
    {
        protected static PriceTresholdBaseDto GetPriceTreshold()
        {
            return new PriceTresholdBaseDto
            {
                MinCharges = 5,
                PricePerCharge = 5.5m
            };
        }
    }
}
