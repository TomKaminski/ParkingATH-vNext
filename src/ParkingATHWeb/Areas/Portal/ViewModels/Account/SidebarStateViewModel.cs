using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Account
{
    public class SidebarStateViewModel : SmartParkBaseViewModel
    {
        [Required]
        public bool SidebarShrinked { get; set; }
    }
}
