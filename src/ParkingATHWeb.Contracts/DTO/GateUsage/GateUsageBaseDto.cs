using System;
using ParkingATHWeb.Contracts.Common;

namespace ParkingATHWeb.Contracts.DTO.GateUsage
{
    public class GateUsageBaseDto:BaseDto<Guid>
    {
        public DateTime DateOfUse { get; set; }
        public int UserId { get; set; }
    }
}
