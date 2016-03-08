using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Chart
{
    public class ChartDataReturnModel
    {
        public string[] Labels { get; set; }
        public int[] Data { get; set; }
        public ChartType Type { get; set; }
        public ChartGranuality Granuality { get; set; }
        public int UserId { get; set; }
    }
}
