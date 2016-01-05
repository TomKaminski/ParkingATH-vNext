using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.ViewModels.PriceTreshold
{
    public class AdminPriceTresholdListItemViewModel : ParkingAthListBaseViewModel
    {
        public int Id { get; set; }
        public int MinCharges { get; set; }
        public decimal PricePerCharge { get; set; }
    }
}
