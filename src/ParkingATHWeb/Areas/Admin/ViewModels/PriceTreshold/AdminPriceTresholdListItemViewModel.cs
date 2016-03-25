using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.ViewModels.PriceTreshold
{
    public class AdminPriceTresholdListItemViewModel : SmartParkListBaseViewModel
    {
        public int Id { get; set; }
        public int MinCharges { get; set; }
        public decimal PricePerCharge { get; set; }
        public int NumOfOrders { get; set; }
    }
}
