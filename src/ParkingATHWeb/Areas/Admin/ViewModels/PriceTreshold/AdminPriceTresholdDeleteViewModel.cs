using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.ViewModels.PriceTreshold
{
    public class AdminPriceTresholdDeleteViewModel : ParkingAthDeleteBaseViewModel<int>
    {
        public int MinCharges { get; set; }
        public decimal PricePerCharge { get; set; }
    }
}
