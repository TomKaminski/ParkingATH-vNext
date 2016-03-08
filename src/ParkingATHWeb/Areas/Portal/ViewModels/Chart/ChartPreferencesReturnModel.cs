using System;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Chart
{
    public class ChartPreferencesReturnModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ChartType Granuality { get; set; }
        public string LabelStartDate { get; set; }
        public string LabelEndDate { get; set; }
    }
}
