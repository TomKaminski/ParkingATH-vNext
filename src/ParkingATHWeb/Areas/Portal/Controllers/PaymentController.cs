using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]/Platnosci")]
    [Authorize]
    public class PaymentController : BaseController
    {
        private readonly IUserService _userService;

        public PaymentController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
