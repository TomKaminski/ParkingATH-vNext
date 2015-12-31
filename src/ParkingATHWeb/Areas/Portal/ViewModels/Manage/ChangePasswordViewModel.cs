using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.Infrastructure.Attributes;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Manage
{
    public class ChangePasswordViewModel : ParkingAthBaseViewModel
    {
        [ParkingAthRequired]
        public string OldPassword { get; set; }
        [ParkingAthRequired]
        public string Password { get; set; }
        [ParkingAthRequired]
        [Compare("Password", ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "ResetPasswordViewModel_Password_CompareError")]
        public string RepeatPassword { get; set; }
    }
}
