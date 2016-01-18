using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.ApiModels.Account
{
    public class ForgotApiModel
    {
        [Required]
        public string Email { get; set; }
    }
}
