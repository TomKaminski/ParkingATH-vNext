using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.ViewModels.User
{
    public class AdminUserDeleteViewModel:SmartParkDeleteBaseViewModel<int>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public bool LockedOut { get; set; }
        public int OrdersCount { get; set; }
        public int Charges { get; set; }
    }
}
