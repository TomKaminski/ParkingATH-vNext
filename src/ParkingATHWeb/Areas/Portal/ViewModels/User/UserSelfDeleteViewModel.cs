using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.User
{
    public class UserSelfDeleteViewModel
    {
        [Required]
        public string Password { get; set; }
    }
}