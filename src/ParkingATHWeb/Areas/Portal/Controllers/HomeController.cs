using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.Controllers.Base;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]")]
    public class HomeController : BaseController
    {
        [Route("~/[area]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
