using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Account
{
    public class RegisterViewModel : ParkingAthBaseViewModel
    {
        [EmailAddress]
        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        [MinLength(8)]
        [PasswordPropertyText]
        [DisplayName("Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        [Compare("Password")]
        [PasswordPropertyText]
        [DisplayName("Powtórz hasło")]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        [DisplayName("Imię")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
    }
}