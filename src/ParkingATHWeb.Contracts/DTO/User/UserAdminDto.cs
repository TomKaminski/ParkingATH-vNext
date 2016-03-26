using System.Collections.Generic;
using System.Security.AccessControl;
using ParkingATHWeb.Contracts.DTO.Order;

namespace ParkingATHWeb.Contracts.DTO.User
{
    public class UserAdminDto : UserBaseDto
    {
        public int OrdersCount { get; set; }
        public int GateUsagesCount { get; set; }
        public IEnumerable<OrderBaseDto> Orders { get; set; }
        public string ImgId { get; set; }
    }
}
