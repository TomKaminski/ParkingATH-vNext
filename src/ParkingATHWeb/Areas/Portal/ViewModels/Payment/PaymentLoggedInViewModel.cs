using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Payment
{
    public class PaymentLoggedInViewModel
    {
        [Required]
        public int Charges { get; set; }

    }
}
