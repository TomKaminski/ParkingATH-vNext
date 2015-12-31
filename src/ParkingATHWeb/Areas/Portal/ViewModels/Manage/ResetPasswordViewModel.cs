using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.Infrastructure.Attributes;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Manage
{
    public class ResetPasswordViewModel
    {
        [ParkingAthRequired]
        public string Token { get; set; }
        [ParkingAthRequired]
        public string Password { get; set; }
        [ParkingAthRequired]
        [Compare("Password", ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "ResetPasswordViewModel_Password_CompareError")]
        public string NewPassword { get; set; }
    }
}
