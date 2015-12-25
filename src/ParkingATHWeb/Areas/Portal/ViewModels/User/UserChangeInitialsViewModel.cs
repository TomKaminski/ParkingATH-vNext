using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.User
{
    public class UserChangeInitialsViewModel : ParkingAthBaseViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}