using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.Controllers.Base;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]")]
    [Authorize]
    public class HomeController : BaseController
    {
        [Route("~/[area]")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("~/[area]/Dashboard")]
        public IActionResult Dashboard()
        {
            return PartialView();
        }
    }
}
