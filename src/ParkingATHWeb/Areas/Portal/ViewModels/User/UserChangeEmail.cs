using ParkingATHWeb.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.User
{
    public class UserChangeEmail : ParkingAthBaseViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        [EmailAddress]
        public string NewEmail { get; set; }

        [Compare("NewEmail")]
        public string NewEmailRepeat { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        public string Password { get; set; }
    }
}