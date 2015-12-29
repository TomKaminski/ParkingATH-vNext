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
            switch (decryptedToken.Result.TokenType)
            {
                case TokenType.EmailChangeToken:
                    break;
                case TokenType.PasswordChangeResetToken:
                    break;
                case TokenType.ViewInBrowserToken:
                    return RedirectToAction("Display", "Message", new {id});
            }
            return RedirectToAction("Error", "Home");
        }
    }
}