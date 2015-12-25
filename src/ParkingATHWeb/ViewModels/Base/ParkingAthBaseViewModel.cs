using System.Collections.Generic;

namespace ParkingATHWeb.ViewModels.Base
{
    public class ParkingAthBaseViewModel
    {
        public IEnumerable<string> ValidationErrors { get; set; }

        public void AppendBackendValidationErrors(IEnumerable<string> errors)
        {
            ValidationErrors = errors;
        }
    }
}
