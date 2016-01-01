using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Contracts.Services;
using Microsoft.AspNet.Authorization;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Areas.Portal.ViewModels.Manage;
using ParkingATHWeb.Areas.Portal.ViewModels.User;

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

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var userModel = AutoMapper.Mapper.Map<UserInfoViewModel>((await _userService.GetByEmailAsync(CurrentUser.Email)).Result);
            return View(userModel);
        }

        [Route("ResetowanieHasla")]
        [AllowAnonymous]
        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel { Token = id });
        }

        [Route("ResetowanieHasla")]
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resetPasswordResult = await _userService.ResetPasswordAsync(model.Token, model.Password);
                if (resetPasswordResult.IsValid)
                {

                    return RedirectToAction("Index","Home");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resetPasswordResult = await _userService.ChangePasswordAsync(CurrentUser.Email, model.OldPassword, model.Password);
                if (resetPasswordResult.IsValid)
                {
                    return RedirectToAction("Index","Manage");
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
