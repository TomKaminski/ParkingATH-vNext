using System;
using ParkingATHWeb.Contracts.Common;

namespace ParkingATHWeb.Contracts.DTO.GateUsage
{
    public class GateUsageBaseDto:BaseDto
    {
        public Guid Id { get; set; }
        public DateTime DateOfUse { get; set; }
        public int UserProfileId { get; set; }
    }
}
