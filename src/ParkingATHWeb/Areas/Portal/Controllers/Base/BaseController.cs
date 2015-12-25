using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Models;


namespace ParkingATHWeb.Areas.Portal.Controllers.Base
{
    public class BaseController : Controller
    {
        protected AppUserState AppUserState;

        public BaseController()
        {
            var appUserState = new AppUserState();
            if (User != null)
            {
                var user = User;
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
            AppUserState = appUserState;
            ViewBag.UserState = AppUserState;
        }
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private static string GetClaim(IEnumerable<Claim> claims, string key)
        {
            var claim = claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }

        protected async void IdentitySignout()
        {
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.LoginPath);
        }

        protected async void IdentityReSignin(string name, string lastName, bool isPersistent = false)
        {
            IdentitySignout();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, AppUserState.Email),
                new Claim(ClaimTypes.Name, name),
                new Claim("isAdmin",AppUserState.IsAdmin.ToString()),
                new Claim("LastName",lastName)
            };

            var identity = new ClaimsIdentity(claims, "password");

            await HttpContext.Authentication.SignInAsync("password", new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = isPersistent });
        }
    }
}
