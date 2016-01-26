using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Payment
{
    public class PaymentLoggedInViewModel : SmartParkBaseViewModel
    {
        [Required]
        public int Charges { get; set; }

    }
}
