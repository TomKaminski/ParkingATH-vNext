namespace ParkingATHWeb.Areas.Portal.ViewModels.User
{
    public class UserIndexOrderViewModel
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public int NumOfCharges { get; set; }
        public decimal Price { get; set; }
        public decimal PricePerCharge { get; set; }
        public string OrderPlace { get; set; }
    }
}