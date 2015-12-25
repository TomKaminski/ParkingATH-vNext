using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Account
{
    public class LoginViewModel : ParkingAthBaseViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [PasswordPropertyText]
        [DisplayName("Hasło")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Zapamiętaj moje dane")]
        public bool RemeberMe { get; set; }
    }
}
