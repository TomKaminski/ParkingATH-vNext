using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Mvc.ModelBinding;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.DTO.UserPreferences;
using ParkingATHWeb.Models;
using ParkingATHWeb.ViewModels.Base;


namespace ParkingATHWeb.Areas.Portal.Controllers.Base
{
    public class BaseController : Controller
    {
        protected AppUserState CurrentUser => User == null ? new AppUserState() : new AppUserState(User);

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.RouteData.Values["appUserState"] = CurrentUser;
            base.OnActionExecuted(context);
        }

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

        protected async void IdentityReSignin(UserBaseDto user, UserPreferencesDto userPreferences, bool isPersistent = false)
        {
            IdentitySignout();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("isAdmin", user.IsAdmin.ToString()),
                new Claim("LastName", user.LastName),
                new Claim("SidebarShrinked",userPreferences.ShrinkedSidebar.ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim("photoId", userPreferences.ProfilePhotoId.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = isPersistent });
        }

        protected async Task UpdateClaim(string claimName, object claimValue)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;

            IdentitySignout();

            if (claimValue == null)
            {
                claimValue = "";
            }

            var newClaims = claims.Where(x => x.Type != claimName).ToList();
            newClaims.Add(new Claim(claimName, claimValue.ToString()));

            var identityNew = new ClaimsIdentity(newClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identityNew), new AuthenticationProperties { IsPersistent = true });
        }

        protected string GetAppBaseUrl()
        {
            return Url.Action("Index", "Home", new { area = "Portal" }, "http");
        }

        protected IActionResult ReturnModelWithError<TModel, TServiceResult>(TModel model, TServiceResult serviceResult, ModelStateDictionary modelState)
            where TModel : SmartParkBaseViewModel
            where TServiceResult : ServiceResult
        {
            model.AppendErrors(serviceResult.ValidationErrors);
            model.AppendErrors(GetModelStateErrors(modelState));
            return View(model);
        }

        protected IActionResult ReturnJsonModelWithError<TModel, TServiceResult>(TModel model, TServiceResult serviceResult, ModelStateDictionary modelState)
            where TModel : SmartParkBaseViewModel
            where TServiceResult : ServiceResult
        {
            model.AppendErrors(serviceResult.ValidationErrors);
            model.AppendErrors(GetModelStateErrors(modelState));
            return Json(model);
        }

        protected IEnumerable<string> GetModelStateErrors(ModelStateDictionary modelState)
        {
            var modelStateErrors = new List<string>();
            foreach (var mdl in modelState)
            {
                modelStateErrors.AddRange(mdl.Value.Errors.Select(error => error.ErrorMessage));
            }
            return modelStateErrors;
        }
    }
}
