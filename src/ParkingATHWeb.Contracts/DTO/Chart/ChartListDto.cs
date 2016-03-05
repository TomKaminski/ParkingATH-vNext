using System.Collections.Generic;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.DTO.Chart
{
    public class ChartListDto
    {
        public IEnumerable<ChartElement> Elements { get; set; }
        public ChartType Type { get; set; }
        public ChartGranuality Granuality { get; set; }
        public int UserId { get; set; }
    }
}
