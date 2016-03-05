using System;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.DTO.Chart
{
    public class ChartElement
    {
        public ChartElement(DateTime startDate, int nodeValue, ChartGranuality granuality)
        {
            StartDate = startDate;
            NodeValue = nodeValue;
            switch (granuality)
            {
                case ChartGranuality.PerDay:
                    DateLabel = startDate.ToString("dd.MM");
                    break;
                case ChartGranuality.PerWeek:
                    {
                        var endDate = startDate.AddDays(6);
                        DateLabel = $"{startDate.Day}{startDate.Month}-{endDate.Day}{endDate.Month}";
                        break;

                    }
                case ChartGranuality.PerMonth:
                    break;
            }
        }

        public DateTime StartDate { get; set; }
        public string DateLabel { get; set; }
        public int NodeValue { get; set; }
    }
}
