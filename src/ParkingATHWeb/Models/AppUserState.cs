using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingATHWeb.Models
{
    public class AppUserState
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Name);
        }

        public override string ToString()
        {
            return $"{Name} {LastName}";
        }
    }
}
