using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.ApiModels
{
    public class ChangePasswordApiModel
    {
        [Required]
        public string Email { get; set; }

        [Required, Compare("NewPassword")]
        public string NewPasswordRepeat { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string OldPassword { get; set; }
    }
}
