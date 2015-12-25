using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Account
{
    public class RegisterViewModel
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
        [Compare("Password")]
        [PasswordPropertyText]
        [DisplayName("Powtórz hasło")]
        public string RepeatPassword { get; set; }

        [Required]
        [DisplayName("Imię")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
    }
}