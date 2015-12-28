using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Infrastructure.Filters;
using ParkingATHWeb.Models;


namespace ParkingATHWeb.Areas.Portal.Controllers.Base
{
    [UserState]
    public class BaseController : Controller
    {
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected async void IdentitySignout()
        {
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        protected async void IdentityReSignin(AppUserState user, bool isPersistent = false)
        {
            IdentitySignout();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("isAdmin", user.IsAdmin.ToString()),
                new Claim("LastName", user.LastName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = isPersistent });
        }

        protected string GetAppBaseUrl()
        {
            return Url.Action("Index","Home", new {area="Portal"},"http");
        }
    }
}
