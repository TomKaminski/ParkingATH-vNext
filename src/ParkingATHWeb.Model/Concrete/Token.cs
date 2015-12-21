using System;
using System.Collections.Generic;
using ParkingATHWeb.Model.Common;

namespace ParkingATHWeb.Model.Concrete
{
    public class Token:Entity<long>
    {
        public string EncryptedToken { get; set; }
        public DateTime ValidTo { get; set; }
        public virtual ICollection<User> UserPasswordChangeTokens { get; set; }
        public virtual ICollection<User> UserEmailChangeTokens { get; set; }
    }
}
