using System;
using System.Collections.Generic;

namespace ParkingATHWeb.ViewModels.Base
{
    public class SmartParkListBaseViewModel : SmartParkBaseViewModel
    {
    }

    public class SmartParkListWithDateRangeViewModel<T>
        where T: SmartParkListBaseViewModel
    {
        public IEnumerable<T> ListItems { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
