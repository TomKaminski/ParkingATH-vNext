using System;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.ViewModels.Order
{
    public class AdminOrderListItemViewModel : SmartParkListBaseViewModel
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PricePerCharge { get; set; }
        public int NumOfCharges { get; set; }
        public Guid ExtOrderId { get; set; }
        public OrderPlace OrderPlace { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus OrderState { get; set; }
        public string Initials { get; set; }
    }
}
