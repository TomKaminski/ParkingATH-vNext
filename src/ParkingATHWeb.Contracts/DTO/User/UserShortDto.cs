using ParkingATHWeb.Contracts.Common;

namespace ParkingATHWeb.Contracts.DTO.User
{
    public class UserShortDto : BaseDto<int>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
