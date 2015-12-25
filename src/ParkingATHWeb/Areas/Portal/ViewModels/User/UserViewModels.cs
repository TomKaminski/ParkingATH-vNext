using System.Collections.Generic;
using ParkingATHWeb.Areas.Portal.ViewModels.GateUsage;

namespace ParkingATHWeb.Areas.Portal.ViewModels.User
{
    public class UserIndexViewModel
    {
        public IEnumerable<UserIndexOrderViewModel> Orders { get; set; }
        public IEnumerable<GateOpeningViewModel> GateUsages { get; set; }
        public int Charges { get; set; }
    }
}
