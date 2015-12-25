using System;
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
using Microsoft.AspNet.Http.Authentication;
using ParkingATHWeb.Models;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Wyloguj")]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        [Route("~/[area]/Logowanie")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("~/[area]/Logowanie")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userLoginResult = await _userService.LoginFirstTimeMvcAsync(model.Email, model.Password);
                if (userLoginResult.IsValid)
                {
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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("~/[area]/Rejestracja")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userCreateResult = await _userService.CreateAsync(Mapper.Map<UserBaseDto>(model), model.Password);
                if (userCreateResult.IsValid)
                {
                    //TODO: Sent welcome email
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


        private async Task IdentitySignin(string email, bool isPersistent = false)
        {
            var x = await _userService.GetByEmailAsync(email);
            var userState = Mapper.Map<AppUserState>(x.Result);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userState.Email),
                new Claim(ClaimTypes.Name, userState.Name),
                new Claim("isAdmin",userState.IsAdmin.ToString()),
                new Claim("LastName",userState.LastName)
            };

            var identity = new ClaimsIdentity(claims, "password");

            await HttpContext.Authentication.SignInAsync("password", new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = isPersistent });
        }
    }
}
