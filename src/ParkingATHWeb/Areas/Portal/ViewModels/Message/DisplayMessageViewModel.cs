using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Message
{
    public class DisplayMessageViewModel:ParkingAthBaseViewModel
    {
        public string Title { get; set; }
        public string EmailHtml { get; set; }
    }
}
