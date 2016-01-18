using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.ApiModels.Parking
{
    public class OpenGateApiModel
    {
        [Required]
        public string Email { get; set; }
    }
}
