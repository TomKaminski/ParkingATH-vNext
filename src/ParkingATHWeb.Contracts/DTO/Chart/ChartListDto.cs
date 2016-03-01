using System.Collections.Generic;

namespace ParkingATHWeb.Contracts.DTO.Chart
{
    public class ChartListDto
    {
        public IEnumerable<ChartElement> Elements { get; set; }
    }
}
