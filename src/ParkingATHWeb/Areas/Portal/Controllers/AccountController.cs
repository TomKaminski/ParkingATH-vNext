using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.ViewModels.Account;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services;
using AutoMapper;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http.Authentication;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Contracts.DTO.UserPreferences;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]/Konto")]
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMessageService messageService, IMapper mapper)
        {
            _userService = userService;
            _messageService = messageService;
            _mapper = mapper;
        }

        [Route("Wyloguj")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            IdentitySignout();
            return RedirectToAction("Index", "Home");
        }

        [Route("~/[area]/Logowanie")]
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Portal" });
            }
            return View(new LoginViewModel { ReturnUrl = ReturnUrl });
        }

        [Route("~/[area]/Konto/Start")]
        [AllowAnonymous]
        public IActionResult LoginRegisterForgot(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Portal" });
            }
            return View(new LoginRegisterForgot(ReturnUrl));
        }

        [HttpPost]
        [Route("~/[area]/Logowanie")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userLoginResult = await _userService.LoginAsync(model.Email, model.Password);
                if (userLoginResult.IsValid)
                {
                    await IdentitySignin(userLoginResult.Result, userLoginResult.SecondResult, model.RememberMe);
                }
                model.AppendErrors(userLoginResult.ValidationErrors);
            }
            model.AppendErrors(GetModelStateErrors(ModelState));
            if (model.ReturnUrl == null)
            {
                model.ReturnUrl = Url.Action("Index", "Home", new {area = "Portal"});
            }
            return Json(model);
        }

        [Route("~/[area]/Rejestracja")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Portal" });
            }
            return View();
        }

        [HttpPost]
        [Route("~/[area]/Rejestracja")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userCreateResult = await _userService.CreateAsync(_mapper.Map<UserBaseDto>(model), model.Password);
                if (userCreateResult.IsValid)
                {
                    await _messageService.SendMessageAsync(EmailType.Register, userCreateResult.Result, GetAppBaseUrl());
                    model.AppendNotifications("Twoje konto zostało utworzone pomyślnie, czas się zalogować! :)");
                }
                model.AppendErrors(userCreateResult.ValidationErrors);
            }
            model.AppendErrors(GetModelStateErrors(ModelState));
            return Json(model);
        }

        [AllowAnonymous]
        [Route("ZapomnianeHaslo")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("ZapomnianeHaslo")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var changePasswordTokenResult = await _userService.GetPasswordChangeTokenAsync(model.Email);
                if (changePasswordTokenResult.IsValid)
                {
                    var changePasswordUrl = $"{Url.Action("RedirectFromToken", "Token", null, "http")}?id={changePasswordTokenResult.SecondResult}";
                    await _messageService.SendMessageAsync(EmailType.ResetPassword, changePasswordTokenResult.Result, GetAppBaseUrl(),
                        new Dictionary<string, string> { { "ChangePasswordLink", changePasswordUrl } });
                    model.AppendNotifications("Na podany adres email zostały wysłane dalsze instrukcje.");
                }
                model.AppendErrors(changePasswordTokenResult.ValidationErrors);
            }
            model.AppendErrors(GetModelStateErrors(ModelState));
            return Json(model);
        }

        private async Task IdentitySignin(UserBaseDto user, UserPreferencesDto userPreferences, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("isAdmin",user.IsAdmin.ToString()),
                new Claim("LastName",user.LastName),
                new Claim("SidebarShrinked",userPreferences.ShrinkedSidebar.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = isPersistent });
        }
    }
}
