using System;
using System.Security.Claims;
using System.Security.Principal;
using ParkingATHWeb.Shared.Helpers;

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

        public int? UserId => FindFirst("userId") == null ? null : ToNullable.Convert<int>(FindFirst("userId").Value);
        public string Email => FindFirst(ClaimTypes.NameIdentifier) == null ? null : FindFirst(ClaimTypes.NameIdentifier).Value;
        public string Name => FindFirst(ClaimTypes.Name) == null ? null : FindFirst(ClaimTypes.Name).Value;
        public string LastName => FindFirst("LastName") == null ? null : FindFirst("LastName").Value;
        public bool IsAdmin => Convert.ToBoolean(FindFirst("isAdmin") == null ? null : FindFirst("isAdmin").Value);
        public bool SidebarShrinked => Convert.ToBoolean(FindFirst("SidebarShrinked") == null ? null : FindFirst("SidebarShrinked").Value);

        public Guid? PhotoId
        {
            get
            {
                if (FindFirst("photoId") == null || FindFirst("photoId").Value == "")
                {
                    return null;
                }
                return new Guid(FindFirst("photoId").Value);
            }
        }


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
