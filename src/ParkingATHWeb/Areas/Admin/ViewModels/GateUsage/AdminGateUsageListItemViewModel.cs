using System;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.ViewModels.GateUsage
{
    public class AdminGateUsageListItemViewModel : SmartParkListBaseViewModel
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Initials { get; set; }
    }
}
