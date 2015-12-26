using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc.Filters;
using ParkingATHWeb.Models;

namespace ParkingATHWeb.Infrastructure.Filters
{
    public class UserStateAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var appUserState = new AppUserState();
            if (context.HttpContext.User != null)
            {
                var user = context.HttpContext.User;
                var claims = user.Claims.ToList();

                var name = GetClaim(claims, ClaimTypes.Name);
                var email = GetClaim(claims, ClaimTypes.NameIdentifier);
                var isAdmin = GetClaim(claims, "isAdmin");
                var lastName = GetClaim(claims, "LastName");
                appUserState.Name = name;
                appUserState.LastName = lastName;
                appUserState.Email = email;
                appUserState.IsAdmin = Convert.ToBoolean(isAdmin);
            }
            context.RouteData.Values["appUserState"] = appUserState;
            base.OnActionExecuting(context);
        }

        private static string GetClaim(IEnumerable<Claim> claims, string key)
        {
            var claim = claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }
    }
}
