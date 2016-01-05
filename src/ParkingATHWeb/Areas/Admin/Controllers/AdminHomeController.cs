using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Admin.Controllers.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminHomeController : AdminBaseController
    {
        [Route("~/[area]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
