using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.ApiModels.Parking
{
    public class RefreshChargesApiModel
    {
        [Required]
        public string Email { get; set; }
    }
}
