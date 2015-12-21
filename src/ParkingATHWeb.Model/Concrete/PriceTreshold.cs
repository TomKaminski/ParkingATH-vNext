using System.Collections.Generic;
using ParkingATHWeb.Model.Common;

namespace ParkingATHWeb.Model.Concrete
{
    public class PriceTreshold : Entity<int>
    {
        public PriceTreshold()
        {
            Orders = new HashSet<Order>();
        }

        public int MinCharges { get; set; }
        public decimal PricePerCharge { get; set; }

        public virtual HashSet<Order> Orders { get; set; }
    }
}
