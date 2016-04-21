using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.ApiModels.Panel
{
    public class CheckAccountApiModel
    {
        [Required]
        public string Email { get; set; }
    }
}
