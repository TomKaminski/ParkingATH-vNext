using ParkingATHWeb.Areas.Portal.ViewModels.User;

namespace ParkingATHWeb.Areas.Admin.ViewModels.AdminUser
{
    public class AdminUserListItemViewModel: UserBaseViewModel
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public bool LockedOut { get; set; }
        public int OrdersCount { get; set; }
    }
}
