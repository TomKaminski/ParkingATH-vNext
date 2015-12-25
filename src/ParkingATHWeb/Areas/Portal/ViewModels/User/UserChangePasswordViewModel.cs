using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.User
{
    public class UserChangePasswordViewModel
    {
        [Display(Name = "Has³o")]
        [Required(ErrorMessage = "Pole {0} jest wymagane")]
        public string Password { get; set; }
        [Display(Name = "Nowe has³o")]
        [Required(ErrorMessage = "Pole {0} jest wymagane")]
        [MinLength(8, ErrorMessage = "Has³o musi mieæ minimum {0} znaków")]
        [MaxLength(20, ErrorMessage = "Has³o mo¿e mieæ maksimum {0} znaków")]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string RepeatNewPassword { get; set; }
    }
}