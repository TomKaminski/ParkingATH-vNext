using System;
using ParkingATHWeb.Model.Common;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Model.Concrete
{
    public class Order : Entity<long>
    {
        public decimal Price { get; set; }
        public int NumOfCharges { get; set; }
        public Guid ExtOrderId { get; set; }
        public OrderPlace OrderPlace { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus OrderState { get; set; }

        public int UserProfileId { get; set; }
        public int PriceTresholdId { get; set; }

        public virtual PriceTreshold PriceTreshold {get; set; }
        public virtual User User { get; set; }
    }
}
