using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Payment
{
    public class PaymentNotLoggedInViewModel : ParkingAthBaseViewModel
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