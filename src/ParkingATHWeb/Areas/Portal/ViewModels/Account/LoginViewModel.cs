using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Account
{
    public class LoginViewModel
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
