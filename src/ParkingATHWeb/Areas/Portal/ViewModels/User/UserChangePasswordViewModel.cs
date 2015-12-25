using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.User
{
    public class UserChangePasswordViewModel
    {
        [Display(Name = "Has�o")]
        [Required(ErrorMessage = "Pole {0} jest wymagane")]
        public string Password { get; set; }
        [Display(Name = "Nowe has�o")]
        [Required(ErrorMessage = "Pole {0} jest wymagane")]
        [MinLength(8, ErrorMessage = "Has�o musi mie� minimum {0} znak�w")]
        [MaxLength(20, ErrorMessage = "Has�o mo�e mie� maksimum {0} znak�w")]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string RepeatNewPassword { get; set; }
    }
}