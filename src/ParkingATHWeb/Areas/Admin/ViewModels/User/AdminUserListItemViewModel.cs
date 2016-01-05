using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.ViewModels.AdminUser
{
    public class AdminUserListItemViewModel: ParkingAthListBaseViewModel
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public bool LockedOut { get; set; }
        public int OrdersCount { get; set; }
    }
}
