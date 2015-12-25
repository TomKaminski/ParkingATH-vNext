using ParkingATHWeb.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.User
{
    public class UserChangeEmail : ParkingAthBaseViewModel
    {
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }

        [Compare("NewEmail")]
        public string NewEmailRepeat { get; set; }

        [Required]
        public string Password { get; set; }
    }
}