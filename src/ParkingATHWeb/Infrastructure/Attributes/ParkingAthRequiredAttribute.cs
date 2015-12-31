using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Infrastructure.Attributes
{
    public class ParkingAthRequiredAttribute:RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageResourceName,name);
        }
    }
}
