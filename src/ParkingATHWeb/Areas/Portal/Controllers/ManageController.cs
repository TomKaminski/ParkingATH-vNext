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
    public class ManageController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public ManageController(IUserService userService, IMessageService messageService)
        {
            _userService = userService;
            _messageService = messageService;
        }


        [AllowAnonymous]
        public IActionResult ResetPassword(string id)
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string id)
        {
            return View();
        }
    }
}
