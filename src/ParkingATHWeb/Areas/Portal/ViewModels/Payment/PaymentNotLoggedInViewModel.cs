using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Payment
{
    public class PaymentNotLoggedInViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Charges { get; set; }
    }
}