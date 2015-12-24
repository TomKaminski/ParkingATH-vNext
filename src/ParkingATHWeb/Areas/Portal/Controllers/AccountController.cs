using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Contracts.Services;

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
            return RedirectToAction("Index","Home");
        }

        [Route("~/[area]/Logowanie")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("~/[area]/Rejestracja")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
