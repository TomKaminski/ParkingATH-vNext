using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.ViewModels.Base
{
    public class ParkingAthEditBaseViewModel<T> : ParkingAthBaseViewModel
        where T : struct
    {
        [Required(ErrorMessageResourceType = typeof(ViewModelResources), ErrorMessageResourceName = "Common_RequiredError")]
        public T Id { get; set; }
    }
}
