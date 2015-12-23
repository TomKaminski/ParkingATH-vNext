using System;
using ParkingATHWeb.Model.Common;

namespace ParkingATHWeb.Model.Concrete
{
    public class GateUsage : Entity<Guid>
    {
        public DateTime DateOfUse { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
