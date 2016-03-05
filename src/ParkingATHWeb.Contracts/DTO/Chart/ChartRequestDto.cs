using System;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Contracts.DTO.Chart
{
    public class ChartRequestDto
    {
        public ChartRequestDto()
        { 
        }

        public ChartRequestDto(DateTime startDate, DateTime endDate, ChartType type, ChartGranuality granuality, int userId)
        {
            DateRange = new DateRange(startDate,endDate);
            Type = type;
            Granuality = granuality;
            UserId = userId;
        }
        public ChartGranuality Granuality { get; set; }
        public ChartType Type { get; set; }
        public DateRange DateRange { get; set; }
        public int UserId { get; set; }
    }
}
