using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Account
{
    public class LoginViewModel : SmartParkBaseViewModel
    {
        [EmailAddress]
        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        [MinLength(6)]
        [PasswordPropertyText]
        [Display(Name = "hasło")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        [Display(Name = "zapamiętaj mnie")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
