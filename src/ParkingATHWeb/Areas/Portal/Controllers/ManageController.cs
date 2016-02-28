using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Contracts.Services;
using Microsoft.AspNet.Authorization;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Areas.Portal.ViewModels.Account;
using ParkingATHWeb.Areas.Portal.ViewModels.Manage;
using ParkingATHWeb.Areas.Portal.ViewModels.User;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Infrastructure.Attributes;
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
        private readonly IMapper _mapper;

        public ManageController(IUserService userService, IMessageService messageService, IUserPreferencesService userPreferencesService, IMapper mapper)
        {
            _userService = userService;
            _messageService = messageService;
            _userPreferencesService = userPreferencesService;
            _mapper = mapper;
        }

        [Route("")]
        public IActionResult Index()
        {
            return PartialView();
        }

        [Route("GetSettingsIndexData")]
        public async Task<IActionResult> GetSettingsIndexData()
        {
            var serviceResult = await _userService.GetUserDataWithLastGateUsage(CurrentUser.UserId.Value);
            if (serviceResult.IsValid)
            {
                var viewModel = _mapper.Map<UserBaseViewModel>(serviceResult.Result);
                viewModel.LastGateOpenDate = serviceResult.SecondResult != null
                    ? $"{serviceResult.SecondResult.DateOfUse.ToLongDateString()} - {serviceResult.SecondResult.DateOfUse.ToShortTimeString()}"
                    : "Brak wyjazdów";
                return Json(SmartJsonResult<UserBaseViewModel>.Success(viewModel));
            }
            return Json(SmartJsonResult<UserBaseViewModel>.Failure(serviceResult.ValidationErrors));
        }

        [HttpPost]
        [ValidateAntiForgeryTokenFromHeader]
        [Route("SelfDeleteStart")]
        public async Task<IActionResult> SelfDeleteStart()
        {
            var selfDeleteTokenGetResult = await _userService.GetSelfDeleteTokenAsync(CurrentUser.Email);
            if (selfDeleteTokenGetResult.IsValid)
            {
                //TODO: IIS do not accept SomeCtrl/SomeAction/THISID -> we have to use ?id=...
                var selfDeleteUrl = $"{Url.Action("RedirectFromToken", "Token", null, "http")}?id={selfDeleteTokenGetResult.SecondResult}";

                var messageSentResult = await _messageService.SendMessageAsync(EmailType.SelfDelete, selfDeleteTokenGetResult.Result, GetAppBaseUrl(),
                    new Dictionary<string, string> { { "SelfDeleteLink", selfDeleteUrl } });
                if (messageSentResult.IsValid)
                {
                    return
                        Json(
                            SmartJsonResult.Success(
                                "Na adres email powiązany z kontem została wysłana wiadomość z dalszymi instrukcjami"));
                }
                return Json(SmartJsonResult.Failure(messageSentResult.ValidationErrors));
            }
            return Json(SmartJsonResult.Failure(selfDeleteTokenGetResult.ValidationErrors));
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

        [Route("ChangePassword")]
        [HttpPost]
        [ValidateAntiForgeryTokenFromHeader]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));

            var changePasswordResult = await _userService.ChangePasswordAsync(CurrentUser.Email, model.OldPassword, model.Password);

            return Json(changePasswordResult.IsValid
                ? SmartJsonResult.Success("Hasło zostało pomyślnie zmienione!")
                : SmartJsonResult.Failure(changePasswordResult.ValidationErrors));
        }

        [Route("ChangeEmail")]
        [HttpPost]
        [ValidateAntiForgeryTokenFromHeader]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));

            var changePasswordResult = await _userService.ChangeEmailAsync(CurrentUser.Email, model.NewEmail, model.Password);
            if (changePasswordResult.IsValid)
            {
                IdentityReSignin(changePasswordResult.Result, changePasswordResult.SecondResult);
                return Json(SmartJsonResult.Success("Adres email został zmieniony pomyślnie!"));
            }
            return Json(SmartJsonResult.Failure(changePasswordResult.ValidationErrors));
        }

        [Route("SendCharges")]
        [HttpPost]
        [ValidateAntiForgeryTokenFromHeader]
        public async Task<IActionResult> SendCharges([FromBody] SendChargesViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));

            var sendChargesResult = await _userService.TransferCharges(CurrentUser.Email, model.ReceiverEmail, model.AmountOfCharges, model.Password);
            if (sendChargesResult.IsValid)
            {
                return Json(SmartJsonResult<int>.Success(sendChargesResult.Result, "Adres email został zmieniony pomyślnie!"));
            }
            return Json(SmartJsonResult.Failure(sendChargesResult.ValidationErrors));
        }

        [Route("ChangeUserInfo")]
        [HttpPost]
        [ValidateAntiForgeryTokenFromHeader]
        public async Task<IActionResult> ChangeUserInfo([FromBody] ChangeUserInfoViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));

            var dto = _mapper.Map<UserBaseDto>(model);
            dto.Email = CurrentUser.Email;
            var editUserResult = await _userService.EditStudentInitialsAsync(dto);
            if (editUserResult.IsValid)
            {
                IdentityReSignin(editUserResult.Result, editUserResult.SecondResult);
                return Json(SmartJsonResult<UserBaseDto>.Success(editUserResult.Result, "Dane kontaktowe zostały zmienione pomyślnie!"));
            }

            return Json(SmartJsonResult.Failure(editUserResult.ValidationErrors));

        }

        [HttpPost]
        [Route("SaveSidebarState")]
        [ValidateAntiForgeryTokenFromHeader]
        public async Task<IActionResult> SaveSidebarState([FromBody] SidebarStateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userFullGetResult = await _userService.GetByEmailWithPreferencesAsync(CurrentUser.Email);
                if (userFullGetResult.IsValid)
                {
                    userFullGetResult.SecondResult.ShrinkedSidebar = model.SidebarShrinked;
                    await _userPreferencesService.EditAsync(userFullGetResult.SecondResult);
                    IdentityReSignin(userFullGetResult.Result, userFullGetResult.SecondResult);
                    return Json(model);
                }
                model.AppendErrors(userFullGetResult.ValidationErrors);
            }
            model.AppendErrors(GetModelStateErrors(ModelState));
            return Json(false);
        }
    }

}
