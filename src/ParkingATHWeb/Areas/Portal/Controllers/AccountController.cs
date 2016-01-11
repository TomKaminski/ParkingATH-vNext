using System.Collections.Generic;
using System.Linq;
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
using ParkingATHWeb.Models;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Contracts.DTO;
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

        public AccountController(IUserService userService, IMessageService messageService)
        {
            _userService = userService;
            _messageService = messageService;
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
            return View(new LoginViewModel {ReturnUrl = ReturnUrl});
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
                    await IdentitySignin(userLoginResult.Result, model.RemeberMe);
                    return RedirectToLocal(model.ReturnUrl);
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    //TODO: Fajne noty z błędem/błędami systemowymi!!!!!!!!!!!! :D
                    model.AppendBackendValidationErrors(userLoginResult.ValidationErrors);

                    ModelState.AddModelError("", userLoginResult.ValidationErrors.First());
                    return View(model);
                }
            }
            return View(model);
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
                var userCreateResult = await _userService.CreateAsync(Mapper.Map<UserBaseDto>(model), model.Password);
                if (userCreateResult.IsValid)
                {
                    await _messageService.SendMessageAsync(EmailType.Register, userCreateResult.Result, GetAppBaseUrl());
                    return RedirectToAction("Login");
                }
                else
                {
                    //TODO: Fajne noty z błędem/błędami systemowymi!!!!!!!!!!!! :D
                    model.AppendBackendValidationErrors(userCreateResult.ValidationErrors);

                    ModelState.AddModelError("", userCreateResult.ValidationErrors.First());
                    return View(model);
                }
            }
            return View(model);
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
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var changePasswordTokenResult = await _userService.GetPasswordChangeTokenAsync(email);

            //TODO: IIS do not accept SomeCtrl/SomeAction/THISID -> we have to use ?id=...
            var changePasswordUrl = $"{Url.Action("RedirectFromToken", "Token", null, "http")}?id={changePasswordTokenResult.SecondResult}";
            await _messageService.SendMessageAsync(EmailType.Register, changePasswordTokenResult.Result, GetAppBaseUrl(),
                new Dictionary<string, string> { { "ChangePasswordLink", changePasswordUrl } });
            return RedirectToAction("Index", "Home");
        }

        private async Task IdentitySignin(UserBaseDto user, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("isAdmin",user.IsAdmin.ToString()),
                new Claim("LastName",user.LastName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = isPersistent });
        }
    }
}
