using System;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.ViewModels.GateUsage
{
    public class AdminGateUsageListItemViewModel : ParkingAthListBaseViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateOfUse { get; set; }
    }
}
