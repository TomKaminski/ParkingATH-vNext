using System.Collections.Generic;
using System.Linq;

namespace ParkingATHWeb.ViewModels.Base
{
    public abstract class ParkingAthBaseViewModel
    {
        public IEnumerable<string> ValidationErrors { get; set; }

        public bool IsValid => ValidationErrors == null || !ValidationErrors.Any();

        public void AppendBackendValidationErrors(IEnumerable<string> errors)
        {
            ValidationErrors = errors;
        }
    }
}
