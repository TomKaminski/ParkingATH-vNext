using ParkingATHWeb.Model.Common;

namespace ParkingATHWeb.Model.Concrete
{
    public class UserPreferences :Entity<int>
    {
        public bool ShrinkedSidebar { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
