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
    [Route("[area]/[controller]")]
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
        public IActionResult Logout()
        {
            IdentitySignout();
            return RedirectToAction("Index", "Home");
        }

        [Route("~/[area]/Logowanie")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Portal" });
            }
            return View();
        }

        [HttpPost]
        [Route("~/[area]/Logowanie")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            _messageService.SendMessage(new MessageDto
            {
                Type = EmailType.Register
            }, new Dictionary<string, string>
            {
                {"Name","Tomasz"}
            });
            if (ModelState.IsValid)
            {
                var userLoginResult = await _userService.LoginFirstTimeMvcAsync(model.Email, model.Password);
                if (userLoginResult.IsValid)
                {
                    await IdentitySignin(userLoginResult.Result, model.RemeberMe);
                    return RedirectToAction("Index", "Home");
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

                    //TODO: Sent welcome email
                    //_messageService.SendMessage(new MessageDto());
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


        private async Task IdentitySignin(UserBaseDto user, bool isPersistent = false)
        {
            var userState = Mapper.Map<AppUserState>(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userState.Email),
                new Claim(ClaimTypes.Name, userState.Name),
                new Claim("isAdmin",userState.IsAdmin.ToString()),
                new Claim("LastName",userState.LastName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = isPersistent });
        }
    }
}
