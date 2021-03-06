﻿using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]")]
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [Route("Redirect")]
        public IActionResult RedirectFromToken(string id)
        {
            var decryptedToken = _tokenService.GetDecryptedData(id);
            if (decryptedToken.Result.NotExpired())
            {
                switch (decryptedToken.Result.TokenType)
                {
                    case TokenType.ResetPasswordToken:
                        return RedirectToAction("ResetPassword", "Manage", new { id });
                    case TokenType.ViewInBrowserToken:
                        return RedirectToAction("Display", "Message", new { id });
                    case TokenType.SelfDeleteToken:
                        return RedirectToAction("SelfDeleteFinish", "Manage", new { id });
                }
            }
            return RedirectToAction("WrongToken", "Token");
        }

        [Route("InvalidToken")]
        public IActionResult WrongToken()
        {
            return View();
        }
    }
}
