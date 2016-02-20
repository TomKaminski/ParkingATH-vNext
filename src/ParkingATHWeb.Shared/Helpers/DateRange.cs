using System;
using System.Collections.Generic;

namespace ParkingATHWeb.Shared.Helpers
{
    public class DateRange
    {
        public DateRange(DateTime startDate, DateTime endDate)
        {
            Dates = new List<DateTime>();
            var date = startDate;
            do
            {
                Dates.Add(date);
                date = date.AddDays(1);
            } while (date < endDate);
        }
        public List<DateTime> Dates { get; set; }
    }
}
