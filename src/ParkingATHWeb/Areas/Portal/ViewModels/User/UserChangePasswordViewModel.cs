using ParkingATHWeb.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.User
{
    public class UserChangePasswordViewModel : ParkingAthBaseViewModel
    {
        [Display(Name = "Has�o")]
        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        public string Password { get; set; }
        [Display(Name = "Nowe has�o")]
        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        [MinLength(8, ErrorMessage = "Has�o musi mie� minimum {0} znak�w")]
        [MaxLength(20, ErrorMessage = "Has�o mo�e mie� maksimum {0} znak�w")]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string RepeatNewPassword { get; set; }
    }
}