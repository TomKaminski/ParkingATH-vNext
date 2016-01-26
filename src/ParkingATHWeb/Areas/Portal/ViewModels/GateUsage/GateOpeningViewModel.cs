using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.GateUsage
{
    public class GateOpeningViewModel:SmartParkListBaseViewModel
    {
        public string Date { get; set; }
        public string Time { get; set; }
    }
}