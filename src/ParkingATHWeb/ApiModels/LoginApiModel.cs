using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.ApiModels
{
    public class LoginApiModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
