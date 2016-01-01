using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.User
{
    public class UserSelfDeleteViewModel : ParkingAthBaseViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        public string Password { get; set; }
    }
}