using System;
using ParkingATHWeb.Model.Common;

namespace ParkingATHWeb.Model.Concrete
{
    public class Token:Entity<long>
    {
        public string EncryptedToken { get; set; }
        public DateTime ValidTo { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
