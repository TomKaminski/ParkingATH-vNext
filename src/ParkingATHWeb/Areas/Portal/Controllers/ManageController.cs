using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Contracts.Services;
using Microsoft.AspNet.Authorization;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Areas.Portal.ViewModels.Account;
using ParkingATHWeb.Areas.Portal.ViewModels.Manage;
using ParkingATHWeb.Areas.Portal.ViewModels.User;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]/Konto")]
    [Authorize]
    public class ManageController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IUserPreferencesService _userPreferencesService;

        public ManageController(IUserService userService, IMessageService messageService, IUserPreferencesService userPreferencesService)
        {
            _userService = userService;
            _messageService = messageService;
            _userPreferencesService = userPreferencesService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var userModel = Mapper.Map<UserBaseViewModel>((await _userService.GetByEmailAsync(CurrentUser.Email)).Result);
            return PartialView(userModel);
        }

        [Route("UsuwanieKonta")]
        public IActionResult SelfDeleteStart()
        {
            return View();
        }

        [HttpPost]
        [ActionName("SelfDeleteStart")]
        [ValidateAntiForgeryToken]
        [Route("UsuwanieKonta")]
        public async Task<IActionResult> SelfDeleteStartPost()
        {
            var selfDeleteTokenGetResult = await _userService.GetSelfDeleteTokenAsync(CurrentUser.Email);

            //TODO: IIS do not accept SomeCtrl/SomeAction/THISID -> we have to use ?id=...
            var selfDeleteUrl = $"{Url.Action("RedirectFromToken", "Token", null, "http")}?id={selfDeleteTokenGetResult.SecondResult}";
            await _messageService.SendMessageAsync(EmailType.SelfDelete, selfDeleteTokenGetResult.Result, GetAppBaseUrl(),
                new Dictionary<string, string> { { "SelfDeleteLink", selfDeleteUrl } });
            return RedirectToAction("Index", "Home");
        }

        [Route("PotwierdzenieUsunieciaKonta")]
        public IActionResult SelfDeleteFinish(string id)
        {
            return View(new SelfDeleteViewModel { Token = id });
        }

        [Route("PotwierdzenieUsunieciaKonta")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelfDeleteFinish(SelfDeleteViewModel model)
        {
            var selfDeleteResult = await _userService.SelfDeleteAsync(CurrentUser.Email, model.Token);
            if (selfDeleteResult.Result && selfDeleteResult.IsValid)
            {
                IdentitySignout();
                return RedirectToAction("Index", "Home");
            }
            model.AppendErrors(selfDeleteResult.ValidationErrors);

            ModelState.AddModelError("", selfDeleteResult.ValidationErrors.First());
            return View(model);
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
                    model.AppendNotifications("Hasło zostało zmienione pomyślnie.");
                    return Json(model);
                }
                model.AppendErrors(resetPasswordResult.ValidationErrors);
            }
            model.AppendErrors(GetModelStateErrors(ModelState));
            return Json(model);
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
                var changePasswordResult = await _userService.ChangePasswordAsync(CurrentUser.Email, model.OldPassword, model.Password);
                if (changePasswordResult.IsValid)
                {
                    return RedirectToAction("Index", "Manage");
                }
                //TODO: Fajne noty z błędem/błędami systemowymi!!!!!!!!!!!! :D
                model.AppendErrors(changePasswordResult.ValidationErrors);

                ModelState.AddModelError("", changePasswordResult.ValidationErrors.First());
            }
            return View();
        }

        [Route("ZmianaEmaila")]
        public IActionResult ChangeEmail()
        {
            return View();
        }

        [Route("ZmianaEmaila")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resetPasswordResult = await _userService.ChangeEmailAsync(CurrentUser.Email, model.NewEmail, model.Password);
                if (resetPasswordResult.IsValid)
                {
                    IdentitySignout();
                    return RedirectToAction("Login", "Account");
                }
                //TODO: Fajne noty z błędem/błędami systemowymi!!!!!!!!!!!! :D
                model.AppendErrors(resetPasswordResult.ValidationErrors);

                ModelState.AddModelError("", resetPasswordResult.ValidationErrors.First());
            }
            return View(model);
        }

        [Route("PodarujWyjazdy")]
        public IActionResult SendCharges()
        {
            return View();
        }

        [Route("PodarujWyjazdy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCharges(SendChargesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sendChargesResult = await _userService.TransferCharges(CurrentUser.Email, model.RecieverEmail, model.AmountOfCharges, model.Password);

                if (sendChargesResult.IsValid)
                {
                    return RedirectToAction("Index", "Manage");
                }
                //TODO: Fajne noty z błędem/błędami systemowymi!!!!!!!!!!!! :D
                model.AppendErrors(sendChargesResult.ValidationErrors);

                ModelState.AddModelError("", sendChargesResult.ValidationErrors.First());
            }
            return View(model);
        }


        [Route("ZmienDane")]
        public async Task<IActionResult> ChangeUserInfo()
        {
            var userInfoShortData = Mapper.Map<ChangeUserInfoViewModel>((await _userService.GetByEmailAsync(CurrentUser.Email)).Result);
            return View(userInfoShortData);
        }

        [Route("ZmienDane")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserInfo(ChangeUserInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = Mapper.Map<UserBaseDto>(model);
                dto.Email = CurrentUser.Email;
                var editUserResult = await _userService.EditStudentInitialsAsync(dto);
                if (editUserResult.IsValid)
                {
                    IdentityReSignin(editUserResult.Result, editUserResult.SecondResult);
                    return RedirectToAction("Index", "Manage");
                }

                //TODO: Fajne noty z błędem/błędami systemowymi!!!!!!!!!!!! :D
                model.AppendErrors(editUserResult.ValidationErrors);
                ModelState.AddModelError("", editUserResult.ValidationErrors.First());
            }
            return View(model);
        }

        [HttpPost]
        [Route("SaveSidebarState")]
        public async Task<IActionResult> SaveSidebarState([FromBody]SidebarStateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userFullGetResult = await _userService.GetByEmailWithPreferencesAsync(CurrentUser.Email);
                if (userFullGetResult.IsValid)
                {
                    userFullGetResult.SecondResult.ShrinkedSidebar = model.SidebarShrinked;
                    await _userPreferencesService.EditAsync(userFullGetResult.SecondResult);
                    IdentityReSignin(userFullGetResult.Result,userFullGetResult.SecondResult);
                    return Json(model);
                }
                model.AppendErrors(userFullGetResult.ValidationErrors);
            }
            model.AppendErrors(GetModelStateErrors(ModelState));
            return Json(false);
        }
    }

}
