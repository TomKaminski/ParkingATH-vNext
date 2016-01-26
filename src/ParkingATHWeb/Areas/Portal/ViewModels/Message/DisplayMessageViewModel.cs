using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Message
{
    public class DisplayMessageViewModel:SmartParkBaseViewModel
    {
        public string Title { get; set; }
        public string EmailHtml { get; set; }
    }
}
