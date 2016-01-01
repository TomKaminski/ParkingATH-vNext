using ParkingATHWeb.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.User
{
    public class UserChangePasswordViewModel : ParkingAthBaseViewModel
    {
        [Display(Name = "Has³o")]
        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        public string Password { get; set; }
        [Display(Name = "Nowe has³o")]
        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        [MinLength(8, ErrorMessage = "Has³o musi mieæ minimum {0} znaków")]
        [MaxLength(20, ErrorMessage = "Has³o mo¿e mieæ maksimum {0} znaków")]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string RepeatNewPassword { get; set; }
    }
}