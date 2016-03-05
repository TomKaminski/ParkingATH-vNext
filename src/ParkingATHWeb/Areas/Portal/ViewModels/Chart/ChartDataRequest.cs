using System;
using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.Infrastructure.Attributes;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Chart
{
    public class ChartDataRequest
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [IsDateAfter("StartDate", true, ErrorMessage = "Data końcowa musi być równa lub późniejsza od daty początkowej.")]
        public DateTime EndDate { get; set; }

        [Required]
        public ChartGranuality Granuality { get; set; }

        [Required]
        public ChartType Type { get; set; }
    }
}
