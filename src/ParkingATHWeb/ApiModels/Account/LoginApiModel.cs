using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.ApiModels.Account
{
    public class LoginApiModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
