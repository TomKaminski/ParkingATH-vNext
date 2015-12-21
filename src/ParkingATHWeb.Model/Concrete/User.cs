using System;
using System.Collections.Generic;
using ParkingATHWeb.Model.Common;

namespace ParkingATHWeb.Model.Concrete
{
    public class User : Entity<int>
    {
        public User()
        {
            Orders = new HashSet<Order>();
            GateUsages = new HashSet<GateUsage>();
        }

        public string Email { get; set; }
        public int Charges { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }




        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool LockedOut { get; set; }
        public int UnsuccessfulLoginAttempts { get; set; }
        public DateTime LockedTo { get; set; }



        public long PasswordChangeTokenId { get; set; }
        public long EmailChangeTokenId { get; set; }



        public virtual Token PasswordChangeToken { get; set; }
        public virtual Token EmailChangeToken { get; set; }
        public virtual HashSet<GateUsage> GateUsages { get; set; }
        public virtual HashSet<Order> Orders { get; set; }
    }
}
