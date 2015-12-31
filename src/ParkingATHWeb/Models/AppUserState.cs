using System;
using System.Security.Claims;
using System.Security.Principal;

namespace ParkingATHWeb.Models
{
    public class AppUserState : ClaimsPrincipal
    {
        public AppUserState()
        {

        }
        public AppUserState(IPrincipal principal)
            : base(principal)
        {

        }

        public string Email => FindFirst(ClaimTypes.NameIdentifier) == null ? null : FindFirst(ClaimTypes.NameIdentifier).Value;
        public string Name => FindFirst(ClaimTypes.Name) == null ? null : FindFirst(ClaimTypes.Name).Value;
        public string LastName => FindFirst("LastName") == null ? null : FindFirst("LastName").Value;
        public bool IsAdmin => Convert.ToBoolean(FindFirst("isAdmin") == null ? null : FindFirst("isAdmin").Value);

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
