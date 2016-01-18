using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.ApiModels
{
    public class ChangeEmailApiModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string NewEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
