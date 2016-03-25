using System;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.ViewModels.User
{
    public class AdminUserListItemViewModel: SmartParkListBaseViewModel
    {
        public int Id { get; set; }
        public string Initials { get; set; }
        public string CreateDateLabel { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public int OrdersCount { get; set; }
        public int Charges { get; set; }
        public int GateUsagesCount { get; set; }
    }
}
