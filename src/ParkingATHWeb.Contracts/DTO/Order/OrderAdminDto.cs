using System;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.DTO.Order
{
    public class OrderBaseDto : BaseDto
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public int NumOfCharges { get; set; }
        public Guid ExtOrderId { get; set; }
        public OrderPlace OrderPlace { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus OrderState { get; set; }

        public int UserProfileId { get; set; }
        public int PriceTresholdId { get; set; }
    }
}
