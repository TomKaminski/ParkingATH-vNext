using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Contracts.Services;
using Microsoft.AspNet.Authorization;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Areas.Portal.ViewModels.Manage;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]/Konto")]
    [Authorize]
    public class ManageController : BaseController
    {
        private readonly IUserService _userService;

        public ManageController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("ResetowanieHasla")]
        [AllowAnonymous]
        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel { Token = id });
        }

        [Route("ResetowanieHasla")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resetPasswordResult = await _userService.ResetPasswordAsync(model.Token, model.Password);
                if (resetPasswordResult.IsValid)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    {
                        //TODO: Fajne noty z błędem/błędami systemowymi!!!!!!!!!!!! :D
                        model.AppendBackendValidationErrors(resetPasswordResult.ValidationErrors);

                        ModelState.AddModelError("", resetPasswordResult.ValidationErrors.First());
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        [Route("ZmianaHasla")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("ZmianaHasla")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resetPasswordResult = await _userService.ChangePasswordAsync(CurrentUser.Email, model.OldPassword, model.Password);
                if (resetPasswordResult.IsValid)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    {
                        //TODO: Fajne noty z błędem/błędami systemowymi!!!!!!!!!!!! :D
                        model.AppendBackendValidationErrors(resetPasswordResult.ValidationErrors);

                        ModelState.AddModelError("", resetPasswordResult.ValidationErrors.First());
                        return View(model);
                    }
                }
            }
            return View();
        }
    }
}
